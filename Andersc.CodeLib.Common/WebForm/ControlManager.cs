#region Comments

// ���ܣ��ؼ��Ĺ������ࡣ
// ���ߣ�Anders Cui
// ���ڣ�2007-03-26

// ����޸ģ�Anders Cui
// ����޸����ڣ�2007-03-31
// �޸����ݣ�
// TODO����������Ż���

#endregion

using System;
using System.Collections;
using System.Xml;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Andersc.CodeLib.Common.WebForm
{
    ///<summary>
    /// �ؼ��Ĺ������ࡣÿ����¼form��ʵ����һ��,����󶨿ؼ��е�tooltip���Ժ�������ص�validator��
    ///</summary>
    public sealed class ControlManager
    {
        private Hashtable hashValidator = new Hashtable(); //validator���󼯺ϡ�
        private string helpFilePath; //webform�İ����ļ�·����
        private Page page;
        private IFormCtrlInfo formCtrlInfo;
        private Control containerCtrl; //һ��form��htmlForm �ؼ���������Ϊvalidator�ؼ���������

        #region ���캯��

        /// <summary>
        /// ���캯������form �е�����control ����ӿؼ��İ�����Ϣ��
        /// </summary>
        /// <param name="page">webform object</param>
        public ControlManager(Page page)
        {
            this.page = page;
            formCtrlInfo = SystemCtrlInfoManager.Instance[page.GetType().Name.ToLower()];
            helpFilePath = formCtrlInfo.HelpPage;

            if (formCtrlInfo.AllCtrlInfo.Count > 0)
            {
                foreach (Control ctrl in this.page.Controls)
                {
                    if (ctrl.GetType() == typeof(System.Web.UI.HtmlControls.HtmlForm))
                    {
                        containerCtrl = ctrl;
                        break;
                    }
                }

                foreach (Control ctl in this.page.Controls)
                {
                    this.FillControl(ctl);
                }

                int num = 0;
                if (hashValidator.Count > 0)
                {
                    foreach (object obj in hashValidator.Values)
                    {
                        //��ѭ��
                        IValidator[] validators = obj as IValidator[];
                        for (int i = 0; i < validators.Length; i++)
                        {
                            num++;
                            if (validators[i] != null)
                                this.containerCtrl.Controls.Add((validators[i] as BaseValidator));
                        }
                    }
                }

                if (this.page.Validators.Count != num)
                {
                    foreach (object obj in hashValidator.Values)
                    {
                        //��ѭ��
                        IValidator[] validators = obj as IValidator[];
                        for (int i = 0; i < validators.Length; i++)
                        {
                            if (validators[i] != null)
                                this.page.Validators.Add(validators[i]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ���control ������control,���formCtrlInfo���ж������tooltip��Ϣ��validator������ؼ���
        /// </summary>
        /// <param name="ctl">control object</param>
        /// <remarks>TODO����Ҫ�Ը��ֿؼ�������ϸ������</remarks>
        private void FillControl(Control ctl)
        {


            if (ctl.Controls.Count > 0)
            {
                //TODO : �����е��Զ���ؼ����˵�
                foreach (Control ctrl in ctl.Controls)
                {
                    this.FillControl(ctrl);
                }
            }
            else
            {
                if (ctl.GetType().ToString() != "System.Web.UI.WebControls.Label")
                {
                    ICtrlInfo ci = this.formCtrlInfo[ctl.ClientID] as ICtrlInfo;
                    if (ci != null)
                    {
                        if (ctl is WebControl)
                            (ctl as WebControl).ToolTip = ci.Help;

                        if (ctl is TextBox)
                        {
                            if (ci.CtrlLength != 0)//textbox ��������볤�ȡ�
                            {
                                (ctl as TextBox).MaxLength = ci.CtrlLength;
                                if ((ctl as TextBox).TextMode == TextBoxMode.MultiLine)
                                    (ctl as TextBox).Attributes.Add("onblur", "tbxVallenCheck(" + ctl.ID + ", " + ci.CtrlLength + ", '������󳤶ȣ�')");
                            }

                        }
                        if (ci.Validator != null)
                        {
                            for (int i = 0; i < ci.Validator.Length; i++)
                            {
                                if (ci.Validator[i] != null)
                                {
                                    //									if (!this.page .Validators.Contains(ci.Validator [i]))
                                    //									{
                                    //										this.page .Validators .Add (ci.Validator [i]);
                                    //									}
                                    //test to remove.
                                    (ci.Validator[i] as BaseValidator).ControlToValidate = ctl.ClientID;
                                }
                            }
                            //һ���ؼ���Ӧһ������
                            hashValidator.Add(ctl.ClientID, ci.Validator);
                        }
                    }
                }
            }
        }

        #endregion

        #region �����ڵĹ�������
        /// <summary>
        /// һ��form�����е�Validator�б�
        /// </summary>
        internal ICollection ControlValidators
        {
            get { return this.hashValidator.Values; }
        }
        /// <summary>
        ///  �򿪰����ļ�����صİ�������
        /// </summary>
        internal string HelpFilePath
        {
            get { return helpFilePath; }

        }

        /// <summary>
        /// validator manager������webform
        /// </summary>
        internal Page OwnerPage
        {
            get { return this.page; }
        }

        #endregion

        /// <summary>
        /// Ϊһ���ؼ������ע��Validator
        /// </summary>
        /// <param name="ctrl">web�ؼ�</param>
        /// <param name="validator">The validator.</param>
        /// <remarks>
        /// //TODO������������,��ֹʹ�á�
        /// </remarks>
        internal void RegisterValidator(Control ctrl, BaseValidator validator)
        {
            validator.ControlToValidate = ctrl.ClientID;

            if (this.hashValidator.ContainsKey(ctrl.ClientID))
            {
            }
            else
            {
                this.containerCtrl.Controls.Add(validator);
                this.hashValidator.Add(ctrl.ClientID, validator);
            }
        }

        public void LoadSpecialValidator(ListControl ctrl)
        {
            this.LoadSpecialValidator(ctrl, "", "");
        }

        public void LoadSpecialValidator(ListControl ctrl, string compareValue, string tipStr)
        {
            BaseValidator validator = SystemCtrlInfoManager.Instance.NewValidator(tipStr, SystemCtrlInfoManager.ValidatorType.CompareValidator);
            (validator as CompareValidator).ValueToCompare = compareValue;
            (validator as CompareValidator).Operator = ValidationCompareOperator.NotEqual;
            this.RegisterValidator(ctrl, validator);
        }

        /// <summary>
        /// �������ĳ�ؼ���Validator�ؼ�
        /// </summary>
        /// <param name="ctrl">�ؼ�����</param>
        /// <returns>Validatorr����</returns>
        internal IValidator GetValidator(Control ctrl)
        {
            if (hashValidator.ContainsKey(ctrl.ClientID))
            {
                return (IValidator)hashValidator[ctrl.ClientID];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ���ĳ�ؼ���validator�����ô�����Ϣ�����顣
        /// </summary>
        /// <param name="ctrl">Windows�ؼ�����</param>
        /// <param name="errMsg">������Ϣ��</param>
        internal void SetErrorMsg(Control ctrl, string errMsg)
        {
            BaseValidator validator = (BaseValidator)hashValidator[ctrl.ClientID];
            if (null != validator)
            {
                validator.ErrorMessage = errMsg;
            }
        }
    }
}