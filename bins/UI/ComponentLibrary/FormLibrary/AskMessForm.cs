using Core.CacheLibrary.FormCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UI.ComponentLibrary.MethodLibrary.Interface;

namespace UI.ComponentLibrary.FormLibrary {
    /// <summary>
    /// 消息提示窗体
    /// </summary>
    public partial class AskMessForm : Form,IComponentInitMode<Form> {
        public AskMessForm() {
            InitializeComponent();
        }

        /// <summary>
        /// 打开单例模式下的消息提示窗口
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Form initSingleExample(bool isShowTop) {
            AskMessForm askMessForm = null;
            Form form = FormCacheFactory.getSingletonCache(DefaultNameEnum.ASK_MESS_FORM);
            if(form == null || form.IsDisposed || !(form is AddCharsForm)) { 
                askMessForm = this;
                askMessForm.Name = EnumUtils.GetDescription(DefaultNameEnum.ASK_MESS_FORM);
                askMessForm = FormCacheFactory.ininSingletonForm(askMessForm, false);
            } else {
                askMessForm = (AskMessForm)form;
                askMessForm.Activate();
            }
            if(isShowTop) FormCacheFactory.addTopFormCache(askMessForm);
            askMessForm.Visible = false;
            return askMessForm;
        }
        /// <summary>
        /// 打开多例模式下的消息提示窗口
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Form initPrototypeExample(bool isShowTop) {
            AskMessForm askMessForm = this;
            askMessForm.Name = EnumUtils.GetDescription(DefaultNameEnum.ASK_MESS_FORM)+DateTime.Now.Ticks.ToString();;
            // 加入到顶层窗体集合
            if(isShowTop) FormCacheFactory.addTopFormCache(askMessForm);
            // 加入到多例工厂
            FormCacheFactory.addPrototypeCache(DefaultNameEnum.ASK_MESS_FORM, askMessForm);
            askMessForm.Activate();
            askMessForm.Visible = false;
            return askMessForm;
        }

        private void 取消_but_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
