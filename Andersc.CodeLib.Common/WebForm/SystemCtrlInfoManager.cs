#region Comments

// 功能：管理和存储系统所有界面的控件帮助和出错信息，提供全局唯一实例。
// 作者：Anders Cui
// 日期：2007-03-26

// 最近修改：Anders Cui
// 最近修改日期：2007-03-31
// 修改内容：将实例写入缓存，并建立文件依赖项。

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
    /// 管理和存储系统所有界面的控件帮助和出错信息，全局唯一实例。
    ///</summary>
    ///<remarks>TODO：Check .NET 2.0中验证控件的新特性；使用泛型来提高性能。</remarks>
    internal sealed class SystemCtrlInfoManager
    {
        // 校验类型枚举
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
        /// 返回本类的唯一实例。
        /// </summary>
        /// <remarks>还需要改进，如多线程的情况。</remarks>
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

        #region 私有构造函数

        /// <summary>
        /// 将页面验证信息加入到内存。
        /// </summary>
        private SystemCtrlInfoManager()
        {
            XmlDocument doc = XmlHelper.GetXmlDocument(path);
            // TODO: 添加校验文件功能
            //XmlHelper.Validate(path, schemaPath);

            XmlNodeList nl = doc.SelectNodes("/root/Form");
            
            // 遍历每个Form（页面）
            foreach (XmlNode node in nl)
            {
                FormCtrlInfo fci = new FormCtrlInfo(node.Attributes["name"].Value.ToLower(), node.Attributes["help"].Value);
                this.allForm.Add(fci.FormName, fci);
                XmlNodeList childNodes = node.SelectNodes("Control");

                // 遍历每个Control（控件）
                foreach (XmlNode childNode in childNodes)
                {
                    int length = 0;
                    if (childNode.Attributes["length"] != null)
                    {
                        length = int.Parse(childNode.Attributes["length"].Value);
                    }

                    // toolTip信息。
                    XmlAttribute ctrlHelp = childNode.Attributes["help"];
                    string toolTip = (ctrlHelp == null) ? null : ctrlHelp.Value;

                    // 输入校验。
                    XmlNodeList ctrlValidators = childNode.SelectNodes("Validator");
                    IValidator[] validators = FillValidatorCtrl(ctrlValidators);

                    CtrlInfo ci = new CtrlInfo(childNode.Attributes["name"].Value, toolTip, validators, length);
                    fci.Add(ci);
                }
            }
        }

        // 根据配置信息生成一个控件的验证控件。
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
        /// 根据配置信息生成一个验证控件。
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
                    errorMsg = "*"; // 校验没有通过的缺省值。
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
                                throw new ArgumentException("只能与控件或固定值，而不能是：" + compareValueInfo[0]);
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
                    throw new ArgumentException("无法识别的校验类型：" + dataType);
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
                    throw new ArgumentException("无法识别的比较类型：" + oper);
            }
            return operType;
        }

        #endregion

        /// <summary>
        /// 注册新的校验控件
        /// </summary>
        /// <param name="errorMsg">校验不通过时的出错提示</param>
        /// <param name="valType">校验类型</param>
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
            // 不显示校验控件。
            validator.Display = ValidatorDisplay.None;
            return validator;
        }

        /// <summary>
        /// 按Form名获得该Form的所有控件的帮助和出错信息对象。
        /// </summary>
        public IFormCtrlInfo this[string formName]
        {
            get
            {
                if (!allForm.Contains(formName))
                {
                    // 如果一个查询没有时，实例化一个，但help文件为空。
                    allForm.Add(formName, new FormCtrlInfo(formName, ""));
                }
                return (IFormCtrlInfo)this.allForm[formName];
            }
        }

        #region 内部私有类定义FormCtrlInfo[继承hashtable ,实现IFormCtrlInfo]，CtrlInfo 实现ICtrlInfo

        /// <summary>
        /// FormCtrlInfo 一个form 的所有control信息集合类
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
        /// ICtrlInfo的实现
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

    #region 相关接口。

    /// <summary>
    /// Form的所有控件的帮助和出错信息的管理接口
    /// </summary>
    public interface IFormCtrlInfo
    {
        /// <summary>
        /// 所有的控件帮助和出错信息对象集合，集合中的每个元素可以强制转换为ICtrlInfo对象
        /// </summary>
        ICollection AllCtrlInfo { get;}
        /// <summary>
        /// 按控件名获得该控件的帮助和出错信息对象
        /// </summary>
        ICtrlInfo this[string ctrlName] { get;}
        /// <summary>
        /// 所属的Form名
        /// </summary>
        string FormName { get;}
        /// <summary>
        /// 该Form的帮助文档名称
        /// </summary>
        string HelpPage { get;}
    }

    /// <summary>
    /// 控件的帮助和出错信息的获取接口
    /// </summary>
    public interface ICtrlInfo
    {
        /// <summary>
        /// 控件名称
        /// </summary>
        string CtrlName { get;}
        /// <summary>
        /// 控件的帮助字符串
        /// </summary>
        string Help { get;}
        /// <summary>
        /// 控件输入校验器集合，一个控件可以帮定多个校验器
        /// </summary>
        IValidator[] Validator { get;}

        /// <summary>
        /// 控件允许的最大长度
        /// </summary>
        int CtrlLength { get;}
    } 

    #endregion
}