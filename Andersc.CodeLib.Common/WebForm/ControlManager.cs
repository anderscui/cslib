#region Comments

// 功能：控件的管理器类。
// 作者：Anders Cui
// 日期：2007-03-26

// 最近修改：Anders Cui
// 最近修改日期：2007-03-31
// 修改内容：
// TODO：构造过程优化。

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
    /// 控件的管理器类。每个记录form中实例化一个,负责绑定控件中的tooltip属性和生成相关的validator。
    ///</summary>
    public sealed class ControlManager
    {
        private Hashtable hashValidator = new Hashtable(); //validator对象集合。
        private string helpFilePath; //webform的帮助文件路径。
        private Page page;
        private IFormCtrlInfo formCtrlInfo;
        private Control containerCtrl; //一个form的htmlForm 控件，用它作为validator控件的容器。

        #region 构造函数

        /// <summary>
        /// 构造函数遍历form 中的所有control ，添加控件的帮助信息。
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
                        //再循环
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
                        //再循环
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
        /// 检查control 及其子control,如果formCtrlInfo中有定义则绑定tooltip信息及validator到这个控件。
        /// </summary>
        /// <param name="ctl">control object</param>
        /// <remarks>TODO：需要对各种控件进行仔细分析。</remarks>
        private void FillControl(Control ctl)
        {


            if (ctl.Controls.Count > 0)
            {
                //TODO : 将所有的自定义控件过滤掉
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
                            if (ci.CtrlLength != 0)//textbox 的最大输入长度。
                            {
                                (ctl as TextBox).MaxLength = ci.CtrlLength;
                                if ((ctl as TextBox).TextMode == TextBoxMode.MultiLine)
                                    (ctl as TextBox).Attributes.Add("onblur", "tbxVallenCheck(" + ctl.ID + ", " + ci.CtrlLength + ", '超过最大长度：')");
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
                            //一个控件对应一个数组
                            hashValidator.Add(ctl.ClientID, ci.Validator);
                        }
                    }
                }
            }
        }

        #endregion

        #region 构件内的公共属性
        /// <summary>
        /// 一个form的所有的Validator列表
        /// </summary>
        internal ICollection ControlValidators
        {
            get { return this.hashValidator.Values; }
        }
        /// <summary>
        ///  打开帮助文件到相关的帮助索引
        /// </summary>
        internal string HelpFilePath
        {
            get { return helpFilePath; }

        }

        /// <summary>
        /// validator manager所属的webform
        /// </summary>
        internal Page OwnerPage
        {
            get { return this.page; }
        }

        #endregion

        /// <summary>
        /// 为一个控件申请和注册Validator
        /// </summary>
        /// <param name="ctrl">web控件</param>
        /// <param name="validator">The validator.</param>
        /// <remarks>
        /// //TODO：方法有问题,禁止使用。
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
        /// 获得属于某控件的Validator控件
        /// </summary>
        /// <param name="ctrl">控件对象</param>
        /// <returns>Validatorr对象</returns>
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
        /// 针对某控件的validator，设置错误信息并交验。
        /// </summary>
        /// <param name="ctrl">Windows控件对象。</param>
        /// <param name="errMsg">错误信息。</param>
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