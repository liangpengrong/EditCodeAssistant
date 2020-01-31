using Core.CacheLibrary.ControlCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.ComponentLibrary.MethodLibrary.Interface;

namespace UI.ComponentLibrary.ControlLibrary {
    public class RedrawPromptMessBut : Button, IComponentInitMode<Control> {
        /// <summary>
        /// 按钮提示消息
        /// </summary>
        public string ButtonMess { get; set;} = null;

        internal RedrawPromptMessBut(){
            // 初始化控件配置
            initControlDefConfig();
        }
        /// <summary>
        /// 打开单例模式下的对象
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Control initSingleExample(bool isShowTop) {
            RedrawPromptMessBut conThis = null;
            Control con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.PROMPT_MESSAGE);
            if(con == null || !(con is RedrawPromptMessBut)) {
                conThis = this;
                conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.PROMPT_MESSAGE);
                ControlCacheFactory.addSingletonCache(conThis);
            } else { 
                conThis = (RedrawPromptMessBut)con;
            }
            
            if(isShowTop) conThis.BringToFront();
            return conThis;
        }
        /// <summary>
        /// 打开多例模式下的对象
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Control initPrototypeExample(bool isShowTop) {
            RedrawPromptMessBut conThis = this;
            conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.PROMPT_MESSAGE)+DateTime.Now.Ticks.ToString();
            if(isShowTop) conThis.BringToFront();
            // 加入到多例工厂
            ControlCacheFactory.addPrototypeCache(DefaultNameEnum.PROMPT_MESSAGE, conThis);
            return conThis;
        }

        // 鼠标移入事件
        protected override void OnMouseEnter(EventArgs e) {
            ToolTip toolTip = ControlsUtils.GetControlMessTip(this, ButtonMess,
            this.Width +2, -4, 10000, Color.White, Color.Black);
            this.Tag = toolTip;
            base.OnMouseEnter(e);
        }
        // 鼠标移出事件
        protected override void OnMouseLeave(EventArgs e) {
            if(this.Tag != null) {
                ToolTip toolTip = (ToolTip)this.Tag;
                toolTip.Dispose();
                toolTip = null;
                this.Tag = null;
            }
            base.OnMouseLeave(e);
        }
        // 初始化默认配置
        private void initControlDefConfig() { 
            this.Name = EnumUtils.GetDescription(DefaultNameEnum.PROMPT_MESSAGE);
            this.Size = new Size(15,15);
            this.FlatStyle = FlatStyle.Flat;
            this.BackColor = Color.Transparent;
            this.ForeColor = this.BackColor;
            this.TabStop = false;
            this.FlatAppearance.BorderColor = Color.White;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.CheckedBackColor = Color.Transparent;
            this.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.Image = Properties.Resources.疑问;
        }
    }
}