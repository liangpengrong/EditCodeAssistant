using Core.CacheLibrary.ControlCache;
using Core.CacheLibrary.FormCache;
using Core.DefaultData.DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.ComponentLibrary.ControlLibrary;
using UI.ComponentLibrary.ControlLibrary.RightMenu;
using UI.ComponentLibrary.FormLibrary;
using UI.ComponentLibrary.MethodLibrary.Interface;
/// <summary>
/// 组件工厂
/// </summary>
namespace UI {
    public class UIComponentFactory {
        private UIComponentFactory() { }
        /// <summary>
        /// 获取单例模式下的窗口
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public static Form getSingleForm(DefaultNameEnum name, bool isShowTop) {
            Form form = FormCacheFactory.getSingletonCache(name);
            if(form == null || form.IsDisposed) { 
                IComponentInitMode<Form> mode = getInitFormRelation(name);
                if(mode != null) form = mode.initSingleExample(isShowTop);
            }
            return form;
        }
        public static Form getSingleForm(DefaultNameEnum name) { 
            return getSingleForm(name, false);
        }
        /// <summary>
        /// 获取多例模式下的窗口
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public static Form getPrototypeForm(DefaultNameEnum name, bool isShowTop) {
            Form form = null;
            IComponentInitMode<Form> mode = getInitFormRelation(name);
            if(mode != null) { 
                form = mode.initPrototypeExample(isShowTop);
            }
            return form;
        }
        public static Form getPrototypeForm(DefaultNameEnum name) { 
            return getPrototypeForm(name, false);
        }
        /// <summary>
        /// 获取单例模式下的控件
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层控件</param>
        /// <returns></returns>
        public static Control getSingleControl(DefaultNameEnum name, bool isShowTop) {
            Control con = ControlCacheFactory.getSingletonCache(name);
            if(con == null || con.IsDisposed) { 
                IComponentInitMode<Control> mode = getInitControlRelation(name);
                if(mode != null) con = mode.initSingleExample(isShowTop);
            }
            return con;
        }
        public static Control getSingleControl(DefaultNameEnum name) { 
            return getSingleControl(name, false);
        }
        /// <summary>
        /// 获取多例模式下的控件
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层控件</param>
        /// <returns></returns>
        public static Control getPrototypeControl(DefaultNameEnum name, bool isShowTop) {
            Control con = null;
            IComponentInitMode<Control> mode = getInitControlRelation(name);
            if(mode != null) { 
                con = mode.initPrototypeExample(isShowTop);
            }
            return con;
        }
        public static Control getPrototypeControl(DefaultNameEnum name) { 
            return getPrototypeControl(name, false);
        }
        private static IComponentInitMode<Form> getInitFormRelation(DefaultNameEnum name) {
            IComponentInitMode<Form> mode = null;
            if(DefaultNameEnum.CREAD_JAVA_ENTITY.Equals(name)) {
                mode = new CreadJavaEntity();
            } else if(DefaultNameEnum.ADD_CHARS_FORM.Equals(name)) {
                mode = new AddCharsForm();
            } else if(DefaultNameEnum.FIND_REPLACE_FORM.Equals(name)) {
                mode = new FindAndReplace();
            } else if(DefaultNameEnum.SPLIT_CHARS_FORM.Equals(name)) {
                mode = new SplitCharsForm();
            } else if(DefaultNameEnum.ROW_GOTO_FORM.Equals(name)) {
                mode = new RowGoToForm();
            } else if(DefaultNameEnum.SET_CODING_FORM.Equals(name)) {
                mode = new SetCodingForm();
            } else if(DefaultNameEnum.THEREOF_FORM.Equals(name)) {
                mode = new ThereofForm();
            }
            return mode;
        }
        private static IComponentInitMode<Control> getInitControlRelation(DefaultNameEnum name) {
            IComponentInitMode<Control> mode = null;
            if(DefaultNameEnum.ADD_PAGE_BUTTON.Equals(name)) {
                mode = new RedrawAddPageBut();
            } else if(DefaultNameEnum.DATA_GRID_VIEW_REDRAW.Equals(name)) {
                mode = new RedrawDataTable();
            } else if(DefaultNameEnum.MAIN_CONTAINER.Equals(name)) {
                mode = new RedrawMainContainer();
            } else if(DefaultNameEnum.TAB_CONTENT.Equals(name)) {
                mode = new RedrawTabControl();
            } else if(DefaultNameEnum.TAB_PAGE_NAME.Equals(name)) {
                mode = new RedrawTabPage();
            } else if(DefaultNameEnum.TEXTBOX_NAME_DEF.Equals(name)) {
                mode = new RedrawTextBox();
            } else if(DefaultNameEnum.TEXT_RIGHT_MENU.Equals(name)) {
                mode = new TextRightMenu();
            } else if(DefaultNameEnum.DATA_VIEW_RIGHT_MENU.Equals(name)) {
                mode = new DataGridViewRightMenu();
            } else if(DefaultNameEnum.TOOL_START.Equals(name)) {
                mode = new RedrawStatusBar();
            } else if(DefaultNameEnum.PROMPT_MESSAGE.Equals(name)) {
                mode = new RedrawPromptMessBut();
            }
            return mode;
        }
    }
}
