#region Comments

// ���ܣ�����ʹ洢ϵͳ���н���Ŀؼ������ͳ�����Ϣ���ṩȫ��Ψһʵ����
// ���ߣ�Anders Cui
// ���ڣ�2007-03-26

// ����޸ģ�Anders Cui
// ����޸����ڣ�2007-03-31
// �޸����ݣ���ʵ��д�뻺�棬�������ļ������

#endregion

using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Andersc.CodeLib.Common.WebForm
{
    ///<summary>
    /// ����ʹ洢ϵͳ���н���Ŀؼ������ͳ�����Ϣ��ȫ��Ψһʵ����
    ///</summary>
    ///<remarks>TODO��Check .NET 2.0����֤�ؼ��������ԣ�ʹ�÷�����������ܡ�</remarks>
    internal sealed class SystemCtrlInfoManager
    {
        // У������ö��
        public enum ValidatorType
        {
            RequiredFieldValidator,
            RangeValidator, 
            CompareValidator,
            RegularExpressionValidator,
            CustomValidator
        }

        private static string path = HttpContext.Current.Server.MapPath(WebConfigManager.GetAppSetting("PageValidatorXmlPath"));
        private static string schemaPath = HttpContext.Current.Server.MapPath(WebConfigManager.GetAppSetting("PageValidatorSchemaPath"));
        private static string CacheName = "ControlInfo";

        private Hashtable allForm = new Hashtable();

        /// <summary>
        /// ���ر����Ψһʵ����
        /// </summary>
        /// <remarks>����Ҫ�Ľ�������̵߳������</remarks>
        internal static SystemCtrlInfoManager Instance
        {
            get
            {
                SystemCtrlInfoManager instance = WebHelper.GetCache(CacheName) as SystemCtrlInfoManager;
                if (instance == null)
                {
                    ReadCtrlInfo();
                    instance = WebHelper.GetCache(CacheName) as SystemCtrlInfoManager;
                }
                return instance;
            }
        }

        private static void ReadCtrlInfo()
        {
            SystemCtrlInfoManager manager = new SystemCtrlInfoManager();
            WebHelper.SetCache(CacheName, manager, path);
        }

        #region ˽�й��캯��

        /// <summary>
        /// ��ҳ����֤��Ϣ���뵽�ڴ档
        /// </summary>
        private SystemCtrlInfoManager()
        {
            XmlDocument doc = XmlHelper.GetXmlDocument(path);
            // TODO: ���У���ļ�����
            //XmlHelper.Validate(path, schemaPath);

            XmlNodeList nl = doc.SelectNodes("/root/Form");
            
            // ����ÿ��Form��ҳ�棩
            foreach (XmlNode node in nl)
            {
                FormCtrlInfo fci = new FormCtrlInfo(node.Attributes["name"].Value.ToLower(), node.Attributes["help"].Value);
                this.allForm.Add(fci.FormName, fci);
                XmlNodeList childNodes = node.SelectNodes("Control");

                // ����ÿ��Control���ؼ���
                foreach (XmlNode childNode in childNodes)
                {
                    int length = 0;
                    if (childNode.Attributes["length"] != null)
                    {
                        length = int.Parse(childNode.Attributes["length"].Value);
                    }

                    // toolTip��Ϣ��
                    XmlAttribute ctrlHelp = childNode.Attributes["help"];
                    string toolTip = (ctrlHelp == null) ? null : ctrlHelp.Value;

                    // ����У�顣
                    XmlNodeList ctrlValidators = childNode.SelectNodes("Validator");
                    IValidator[] validators = FillValidatorCtrl(ctrlValidators);

                    CtrlInfo ci = new CtrlInfo(childNode.Attributes["name"].Value, toolTip, validators, length);
                    fci.Add(ci);
                }
            }
        }

        // ����������Ϣ����һ���ؼ�����֤�ؼ���
        private IValidator[] FillValidatorCtrl(XmlNodeList nodeList)
        {
            if (nodeList == null) { return null; }

            IValidator[] validators = new BaseValidator[nodeList.Count];
            for (int i = 0; i < nodeList.Count; i++)
            {
                validators[i] = FillOneValidatorCtrl(nodeList[i]);
            }

            return validators;
        }

        /// <summary>
        /// ����������Ϣ����һ����֤�ؼ���
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private IValidator FillOneValidatorCtrl(XmlNode node)
        {
            string errorMsg = string.Empty;
            if (node.Attributes["type"] != null)
            {
                string validateType = node.Attributes["type"].Value;
                XmlNode node1 = node.SelectSingleNode("ValidateParamA");
                XmlNode node2 = node.SelectSingleNode("ValidateParamB");
                XmlNode node3 = node.SelectSingleNode("Error");
                if (node3 == null)
                    errorMsg = "*"; // У��û��ͨ����ȱʡֵ��
                else
                    errorMsg = node3.InnerText;

                switch (validateType)
                {//RequiredFieldValidator,RangeValidator,CompareValidator,RegularExpressionValidator
                    case "RequiredFieldValidator":
                        RequiredFieldValidator rfv = NewValidator(errorMsg, ValidatorType.RequiredFieldValidator) as RequiredFieldValidator;
                        if (node1.InnerText.Trim().Length > 0)
                        {
                            rfv.InitialValue = node1.InnerText;
                        }
                        return rfv;
                    //break;
                    case "RangeValidator":
                        if (node1 == null || node2 == null)
                            return null;
                        string type = node1.InnerText;
                        string[] range = node2.InnerText.Split(',');

                        RangeValidator validator1 = NewValidator(errorMsg, ValidatorType.RangeValidator) as RangeValidator;
                        validator1.Type = this.GetValidationType(type);
                        validator1.MinimumValue = range[0];
                        validator1.MaximumValue = range[1];
                        return validator1;
                    //break;
                    case "CompareValidator":
                        if (node1 == null) { return null; }
                        string paramA = node1.InnerText;
                        string paramB = node2.InnerText;
                        string[] compareInfo = paramA.Split(',');
                        string[] compareValueInfo = paramB.Split(',');
                        CompareValidator compareValidator = NewValidator(errorMsg, ValidatorType.CompareValidator) as CompareValidator;
                        compareValidator.Type = this.GetValidationType(compareInfo[0]);
                        compareValidator.Operator = this.GetValidationOperator(compareInfo[1]);
                        if (compareValidator.Operator != ValidationCompareOperator.DataTypeCheck)
                        {
                            if (compareValueInfo[0].Equals("Control"))
                            {
                                compareValidator.ControlToCompare = compareValueInfo[1];
                            }
                            else if (compareValueInfo[0].Equals("Value"))
                            {
                                compareValidator.ValueToCompare = compareValueInfo[1];
                            }
                            else
                            {
                                throw new ArgumentException("ֻ����ؼ���̶�ֵ���������ǣ�" + compareValueInfo[0]);
                            }
                        }
                        return compareValidator;
                    case "RegularExpressionValidator":
                        if (node1 == null)
                            return null;
                        RegularExpressionValidator validator
                            = NewValidator(errorMsg, ValidatorType.RegularExpressionValidator)
                            as RegularExpressionValidator;
                        validator.ValidationExpression = node1.InnerText;
                        return validator;
                    default:
                        return null;
                }
            }
            else
            {
                return null;
            }
        }

        private ValidationDataType GetValidationType(string dataType)
        {
            ValidationDataType valType = ValidationDataType.Date;

            switch (dataType)
            {
                case "Date":
                    valType = ValidationDataType.Date;
                    break;
                case "String":
                    valType = ValidationDataType.String;
                    break;
                case "Currency":
                    valType = ValidationDataType.Currency;
                    break;
                case "Double":
                    valType = ValidationDataType.Double;
                    break;
                case "Integer":
                    valType = ValidationDataType.Integer;
                    break;
                default:
                    throw new ArgumentException("�޷�ʶ���У�����ͣ�" + dataType);
            }
            return valType;
        }
        private ValidationCompareOperator GetValidationOperator(string oper)
        {
            ValidationCompareOperator operType = ValidationCompareOperator.DataTypeCheck;
            switch (oper)
            {
                case "DataTypeCheck":
                    operType = ValidationCompareOperator.DataTypeCheck;
                    break;
                case "Equal":
                    operType = ValidationCompareOperator.Equal;
                    break;
                case "GreaterThan":
                    operType = ValidationCompareOperator.GreaterThan;
                    break;
                case "GreaterThanEqual":
                    operType = ValidationCompareOperator.GreaterThanEqual;
                    break;
                case "LessThan":
                    operType = ValidationCompareOperator.LessThan;
                    break;
                case "LessThanEqual":
                    operType = ValidationCompareOperator.LessThanEqual;
                    break;
                case "NotEqual":
                    operType = ValidationCompareOperator.NotEqual;
                    break;
                default:
                    throw new ArgumentException("�޷�ʶ��ıȽ����ͣ�" + oper);
            }
            return operType;
        }

        #endregion

        /// <summary>
        /// ע���µ�У��ؼ�
        /// </summary>
        /// <param name="errorMsg">У�鲻ͨ��ʱ�ĳ�����ʾ</param>
        /// <param name="valType">У������</param>
        /// <returns></returns>
        public BaseValidator NewValidator(string errorMsg, ValidatorType valType)
        {
            BaseValidator validator = null;
            switch (valType)
            {
                case ValidatorType.RequiredFieldValidator:
                    validator = new RequiredFieldValidator();
                    validator.EnableClientScript = false;
                    break;
                case ValidatorType.RangeValidator:
                    validator = new RangeValidator();
                    (validator as RangeValidator).Type = ValidationDataType.Integer;
                    validator.EnableClientScript = false;
                    break;
                case ValidatorType.RegularExpressionValidator:
                    validator = new RegularExpressionValidator();
                    validator.EnableClientScript = false;
                    break;
                case ValidatorType.CompareValidator:
                    validator = new CompareValidator();
                    validator.EnableClientScript = false;
                    break;
                case ValidatorType.CustomValidator:
                    validator = new CustomValidator();
                    validator.EnableClientScript = false;
                    break;

            }
            validator.ErrorMessage = errorMsg;
            // ����ʾУ��ؼ���
            validator.Display = ValidatorDisplay.None;
            return validator;
        }

        /// <summary>
        /// ��Form����ø�Form�����пؼ��İ����ͳ�����Ϣ����
        /// </summary>
        public IFormCtrlInfo this[string formName]
        {
            get
            {
                if (!allForm.Contains(formName))
                {
                    // ���һ����ѯû��ʱ��ʵ����һ������help�ļ�Ϊ�ա�
                    allForm.Add(formName, new FormCtrlInfo(formName, ""));
                }
                return (IFormCtrlInfo)this.allForm[formName];
            }
        }

        #region �ڲ�˽���ඨ��FormCtrlInfo[�̳�hashtable ,ʵ��IFormCtrlInfo]��CtrlInfo ʵ��ICtrlInfo

        /// <summary>
        /// FormCtrlInfo һ��form ������control��Ϣ������
        /// </summary>
        private class FormCtrlInfo : Hashtable, IFormCtrlInfo
        {
            private readonly string formName, helpPage;

            internal FormCtrlInfo(string formName, string helpPage)
            {
                this.formName = formName;
                this.helpPage = helpPage;
            }

            internal void Add(ICtrlInfo ci)
            {
                base.Add(ci.CtrlName, ci);
            }

            public ICtrlInfo this[string ctrlName]
            {
                get { return (ICtrlInfo)base[ctrlName]; }
            }

            public ICollection AllCtrlInfo
            {
                get { return base.Values; }
            }

            public string FormName
            {
                get { return formName; }
            }
            public string HelpPage
            {
                get { return this.helpPage; }
            }
        }


        /// <summary>
        /// ICtrlInfo��ʵ��
        /// </summary>
        private class CtrlInfo : ICtrlInfo
        {
            private readonly string ctrlName, helpInfo;
            private readonly int ctrlLength;
            private readonly IValidator[] validators; //this maybe a problem eric 7/27
            internal CtrlInfo(string ctrlName,
                string helpInfo,
                IValidator[] validators, int length)
            {
                this.ctrlName = ctrlName;
                this.helpInfo = helpInfo;
                this.ctrlLength = length;
                //ICollection all=null;
                this.validators = validators;
            }

            public string CtrlName { get { return this.ctrlName; } }
            public string Help { get { return this.helpInfo; } }
            public int CtrlLength { get { return this.ctrlLength; } }

            public IValidator[] Validator { get { return this.validators; } }
        }


        #endregion
    }

    #region ��ؽӿڡ�

    /// <summary>
    /// Form�����пؼ��İ����ͳ�����Ϣ�Ĺ���ӿ�
    /// </summary>
    public interface IFormCtrlInfo
    {
        /// <summary>
        /// ���еĿؼ������ͳ�����Ϣ���󼯺ϣ������е�ÿ��Ԫ�ؿ���ǿ��ת��ΪICtrlInfo����
        /// </summary>
        ICollection AllCtrlInfo { get;}
        /// <summary>
        /// ���ؼ�����øÿؼ��İ����ͳ�����Ϣ����
        /// </summary>
        ICtrlInfo this[string ctrlName] { get;}
        /// <summary>
        /// ������Form��
        /// </summary>
        string FormName { get;}
        /// <summary>
        /// ��Form�İ����ĵ�����
        /// </summary>
        string HelpPage { get;}
    }

    /// <summary>
    /// �ؼ��İ����ͳ�����Ϣ�Ļ�ȡ�ӿ�
    /// </summary>
    public interface ICtrlInfo
    {
        /// <summary>
        /// �ؼ�����
        /// </summary>
        string CtrlName { get;}
        /// <summary>
        /// �ؼ��İ����ַ���
        /// </summary>
        string Help { get;}
        /// <summary>
        /// �ؼ�����У�������ϣ�һ���ؼ����԰ﶨ���У����
        /// </summary>
        IValidator[] Validator { get;}

        /// <summary>
        /// �ؼ��������󳤶�
        /// </summary>
        int CtrlLength { get;}
    } 

    #endregion
}