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
    public class RedrawMainContainer : ToolStripContainer, IComponentInitMode<Control> {
        internal RedrawMainContainer() { 
            initContainerDefConfig();
        }
        // 主容器默认配置
        internal void initContainerDefConfig() { 
            ToolStripContainer stripContainer = null;
            Control con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.MAIN_CONTAINER);
            if(con == null) {
                stripContainer = this;
                stripContainer.Name = EnumUtils.GetDescription(DefaultNameEnum.MAIN_CONTAINER);
                stripContainer.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                stripContainer.Dock = DockStyle.None;
                // 设置不被焦点选中
                stripContainer.TabStop = false;
                // 容器的大小
                stripContainer.Size = new Size(1,1);
                stripContainer.Location = new Point(1, 1);
                ControlCacheFactory.addSingletonCache(stripContainer);
            } else { 
                stripContainer = (ToolStripContainer)con;
            }
        }
        /// <summary>
        /// 打开单例模式下的对象
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Control initSingleExample(bool isShowTop) {
            RedrawMainContainer conThis = null;
            Control con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.MAIN_CONTAINER);
            if(con == null || !(con is RedrawMainContainer)) {
                conThis = this;
                conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.MAIN_CONTAINER);
                ControlCacheFactory.addSingletonCache(conThis);
            } else { 
                conThis = (RedrawMainContainer)con;
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
            RedrawMainContainer conThis = this;
            conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.MAIN_CONTAINER)+DateTime.Now.Ticks.ToString();;
            if(isShowTop) conThis.BringToFront();
            // 加入到多例工厂
            ControlCacheFactory.addPrototypeCache(DefaultNameEnum.MAIN_CONTAINER, conThis);
            return conThis;
        }

        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // RedrawMainContainer
            // 
            // 
            // 
            // 
            this.BottomToolStripPanel.BackColor = Color.White;
            this.BottomToolStripPanel.Dock = DockStyle.Bottom;
            this.BottomToolStripPanel.Location = new Point(0, 175);
            this.BottomToolStripPanel.Name = "";
            this.BottomToolStripPanel.Orientation = Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new Size(150, 0);
            // 
            // 
            // 
            this.ContentPanel.BackColor = Color.White;
            this.ContentPanel.Size = new Size(150, 175);
            // 
            // 
            // 
            this.LeftToolStripPanel.BackColor = Color.White;
            this.LeftToolStripPanel.Dock = DockStyle.Left;
            this.LeftToolStripPanel.Location = new Point(0, 0);
            this.LeftToolStripPanel.Name = "";
            this.LeftToolStripPanel.Orientation = Orientation.Vertical;
            this.LeftToolStripPanel.RowMargin = new Padding(0, 3, 0, 0);
            this.LeftToolStripPanel.Size = new Size(0, 175);
            // 
            // 
            // 
            this.RightToolStripPanel.BackColor = Color.White;
            this.RightToolStripPanel.Dock = DockStyle.Right;
            this.RightToolStripPanel.Location = new Point(150, 0);
            this.RightToolStripPanel.Name = "";
            this.RightToolStripPanel.Orientation = Orientation.Vertical;
            this.RightToolStripPanel.RowMargin = new Padding(0, 3, 0, 0);
            this.RightToolStripPanel.Size = new Size(0, 175);
            // 
            // 
            // 
            this.TopToolStripPanel.BackColor = Color.White;
            this.TopToolStripPanel.Dock = DockStyle.Top;
            this.TopToolStripPanel.Location = new Point(0, 0);
            this.TopToolStripPanel.Name = "";
            this.TopToolStripPanel.Orientation = Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new Size(150, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
