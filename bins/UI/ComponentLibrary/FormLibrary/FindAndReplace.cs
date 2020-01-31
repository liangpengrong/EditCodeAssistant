using Core.CacheLibrary.ControlCache;
using Core.CacheLibrary.FormCache;
using Core.CacheLibrary.OperateCache.TextBoxOperateCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UI.ComponentLibrary.MethodLibrary.Interface;

namespace UI.ComponentLibrary.FormLibrary
{
    /// <summary>
    /// 查找替换窗体
    /// </summary>
    public  partial class FindAndReplace : Form, IComponentInitMode<Form> {
        // 启动主窗体
        private Form rootDisplayForm;
        // 要操作的文本框
        private TextBox textBox;
        // 文本框的右键菜单
        private ContextMenuStrip textRightMenu;
        // 查找文本框的文本提示
        private ToolTip findToolTip = null;
        // 替换文本框的文本提示
        private ToolTip repToolTip  =null;

        // 当前点击的是查找还是替换还是全部替换
        private int isFindOrRep = 0;
        // 要查找的是当前文档还是选定内容
        private int isTextOrSelectT = 0;
        // 为选定内容时选定内容的起始位置
        private int selectStatrtI = 0;
        // 要操作的文本
        private string text = "";
        // 向上搜索还是向下搜索
        private int isUpOrDown = 1;
        // 是否需要重置查询方法
        private Boolean isResetFindMet = true;
        // 判断文本框是否被修改过
        private Boolean isTextChang = false;
        // 到达末尾继续
        private Boolean isEndContinue = false;
        // 匹配大小写
        private Boolean isCass = false;
        // 查找的字符串
        private string findChars = "";
        // 替换的字符串
        private string repChars = "";
        // 搜索字符串出现的次数
        private int findIndex = -1;
        // 所有匹配项的索引
        private int[] findIndexArry = new int[] {};
        // 向上搜索是否到达末尾
        private Boolean isFindUpEnd = false;
        // 向下搜索是否到达末尾
        private Boolean isFindDownEnd = false;
        // 向下替换是否到达末尾
        private Boolean isRepDownEnd = false;
        // 是否点击了向上按钮
        private Boolean isfindUpBut = false;
        // 是否点击了向下按钮
        private Boolean isfindDownBut = false;

        // 查找的历史记录
        private List<string> findHistorical = new List<string>();

        // 替换的历史记录
        private List<string> repHistorical = new List<string>();
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="t"></param>
        internal FindAndReplace()
        {
            initData();
            InitializeComponent();
        }
        /// <summary>
        /// 打开单例模式下的查找和替换窗口
        /// <param name="t">所需文本框</param        /// </summary>
        /// <returns></returns>
        public Form initSingleExample(bool isShowTop){
            FindAndReplace findAndReplace = null;
            Form form = FormCacheFactory.getSingletonCache(DefaultNameEnum.FIND_REPLACE_FORM);
            if(form == null || form.IsDisposed || !(form is FindAndReplace)) { 
                findAndReplace = this;
                findAndReplace.Name = EnumUtils.GetDescription(DefaultNameEnum.FIND_REPLACE_FORM);
                // 将窗体放入单例窗体工厂中
                findAndReplace = FormCacheFactory.ininSingletonForm(findAndReplace, false);
            } else {
                findAndReplace = (FindAndReplace) form;
                findAndReplace.Activate();
            }
            if(isShowTop) FormCacheFactory.addTopFormCache(findAndReplace);
            findAndReplace.MinimumSize = findAndReplace.Size;
            findAndReplace.Visible = false;
            return findAndReplace;
        }
        /// <summary>
        /// 打开多例模式下的查找和替换窗口
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Form initPrototypeExample(bool isShowTop) {
            FindAndReplace findAndReplace = this;
            findAndReplace.Name = EnumUtils.GetDescription(DefaultNameEnum.FIND_REPLACE_FORM)+DateTime.Now.Ticks.ToString();;
            // 加入到顶层窗体集合
            if(isShowTop) FormCacheFactory.addTopFormCache(findAndReplace);
            // 加入到多例工厂
            FormCacheFactory.addPrototypeCache(DefaultNameEnum.FIND_REPLACE_FORM, findAndReplace);
            findAndReplace.Activate();
            findAndReplace.Visible = false;
            return findAndReplace;
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        private bool initData() {
            // 获取当前标签中的文本框
            if(textBox == null) { 
                Control conTab = ControlCacheFactory.getSingletonCache(DefaultNameEnum.TAB_CONTENT);
                List<TextBox> controls = null;
                TextBox t = null;
                if (conTab != null && conTab is TabControl && t == null) { 
                    ControlsUtils.GetAllControlByType(ref controls, ((TabControl)conTab).SelectedTab.Controls);
                    if (controls.Count > 0 && controls[0] is TextBox) { 
                        textBox = controls[0];
                    }
                }
            }
            if (textBox != null && textBox is TextBox) {
                if(textBox != null) { 
                    // 重置搜索
                    isResetFindMet = true;
                }
                // 赋值启动主窗体
                rootDisplayForm = textBox.FindForm();
                // 赋值要操作的文本框的右键菜单
                textRightMenu = textBox.ContextMenuStrip;
                // 赋值要操作的文本
                text = textBox.Text;
                return true;
            } else { 
                MessageBox.Show("无法获取文本框");
                return false;    
            }
        }
        /// <summary>
        /// 窗口加载时的执行方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindAndReplace_Load(object sender, EventArgs e)
        {
            // 设置图标
            this.Icon = MessyUtils.IamgeToIcon(Core.ImageResource.查找替换,true);
            this.StartPosition = FormStartPosition.CenterParent;
            if(textBox.SelectionLength == 0) { 
                this.当前文档R.Checked = true;
            } else { 
                this.选定内容R.Checked = true;
            }
            // 调节窗口位置
            Location = FormUtisl.MiddleForm(this);
            // 设置当前索引和总共的索引
            setCurrentLab();
        }

        private void 查找内容T_KeyDown(object sender, KeyEventArgs e){
            TextBox t = (TextBox)sender;
            if(e.Control && e.KeyCode.Equals(Keys.A)) {
                t.SelectAll();
            }
            if(e.KeyCode.Equals(Keys.Escape)) {
                t.SelectionLength = 0;
            }
        }

        private void 替换内容T_KeyDown(object sender, KeyEventArgs e){
            TextBox t = (TextBox)sender;
            if (e.Control && e.KeyCode.Equals(Keys.A))
            {
                t.SelectAll();
            }
        }

        /// <summary>
        /// 重置查询操作
        /// </summary>
        private void resetFind() {
            List<int> l = findOutIntArrMet(text,findChars,isCass);
            findIndexArry = l.ToArray();
            findIndex = -1;
            isFindUpEnd = false;
            isFindDownEnd = false;
        }
        /// <summary>
        /// 搜索执行方法
        /// </summary>
        private void findMet() {
            // 验证
            if(!findCheckMet()) return;
            // 判断文本框内是否被修改过
            if(isTextChang && isFindOrRep == 0) { 
                resetFind();
                isTextChang = false;
            } 
            // 判断是否需要重置搜索
            if(isResetFindMet) { 
                resetFind();
                isResetFindMet = false;
            }
            
            if(isFindOrRep != 0) { 
                if(isfindUpBut && !textBox.SelectedText.Equals(findChars)) findIndex ++;
                if(isfindUpBut) isfindUpBut = false;

                if(isfindDownBut && !textBox.SelectedText.Equals(findChars)) findIndex --;
                if(isfindDownBut) isfindDownBut = false;
            }
            // 判断是否匹配到的内容为0项
            if (!findIndexArry.Length.Equals(0) || findIndex > findIndexArry.Length -1) {
                Boolean isEnd = isFindEndMet();
                //判断是否到达末尾
                if(isEnd) { 
                    // 搜索到达末尾的执行方法
                    findEndMet();
                    return;
                }
                // 判断是向上搜索还是向下搜索
                if(isUpOrDown == 0) { 
                    // 向上搜索
                    findUpMet();
                }else { 
                    // 向下搜索
                    findDownMet();
                }
            } else {
                if(isFindOrRep == 0) MessageBox.Show("找不到匹配项");
            }
        }
        /// <summary>
        /// 判断文本框内容是否被修改过
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private void isTextChangMet() {
            // 判断查找范围是当前文本
            if(isTextOrSelectT == 0 && this.当前文档R.Enabled) { 
                string s = textBox.Text;
                // 判断当前文本框的内容是否不等于要判断的内容
                if(!text.Equals(s)) { 
                    text = s;
                    isTextChang = true;
                }
            }
            // 判断查找范围是所选内容
            if(isTextOrSelectT == 1 && this.选定内容R.Enabled)  { 
                if(!selectStatrtI.Equals(textBox.SelectionStart) && !text.Equals(textBox.SelectedText)
                    && !findChars.Equals(textBox.SelectedText)) { 
                    selectStatrtI = textBox.SelectionStart;
                    text = textBox.SelectedText;
                    isTextChang = true;
                }
            }
            if(this.当前文档R.Checked && !this.当前文档R.Enabled) isTextChang = true;
            if(this.选定内容R.Checked && !this.选定内容R.Enabled) {
                selectStatrtI = 0;
                text = "";
                isTextChang = true;
            } 
        }
        /// <summary>
        /// 向上查找的方法
        /// </summary>
        private void findUpMet() {
            // 将向下搜索到达末尾置为false
            isFindDownEnd = false;
            
            findIndex --;
            // 判断索引是否在可选范围内
            if(findIndex <= findIndexArry.Length-1 && findIndex >= 0) {
                // 判断是当前文档还是选定内容
                isTextOrSelectText();
            } 
        }
        /// <summary>
        /// 向下查找的方法
        /// </summary>
        private void findDownMet() {
            // 将向上搜索到达末尾置为false;
            isFindUpEnd = false;
            
            findIndex ++;
            // 判断索引是否在可选范围内
            if(findIndex <= findIndexArry.Length-1) {
                // 判断是当前文档还是选定内容
                isTextOrSelectText();
            }
        }

        /// <summary>
        /// 判断是当前文档还是选定内容
        /// </summary>
        private void isTextOrSelectText() { 
            int index = findIndexArry[findIndex];
            if(isTextOrSelectT == 0) { 
                textBox.Select(index, findChars.Length);
            } else {
                textBox.Select(index + selectStatrtI, findChars.Length);    
            }
            
        }
        /// <summary>
        /// 遍历文本将全部的匹配项的出现位置放到List中
        /// </summary>
        /// <param name="list">要存放的list</param>
        /// <param name="text">字符串</param>
        /// <param name="findChars">要遍历的字符串</param>
        /// <param name="findNextI">下一个起始位置</param>
        /// <param name="isCase">是否匹配大小写</param>
        private List<int> findOutIntArrMet(string text, string findChars, Boolean isCase) {
            List<int> subIndex = new List<int>();
            int ii = isFindCaseMet(text, findChars, 0, isCase);
            while(ii >= 0 && ii < text.Length)
            {
                subIndex.Add(ii);
                ii = isFindCaseMet(text, findChars, ii+1, isCase);
            }
            return subIndex;
        }

        /// <summary>
        /// 匹配规则的判断方法
        /// </summary>
        /// <returns></returns>
        private int isFindCaseMet(string text, string findStr, int findNextI, Boolean isCase) {
            int index = 0;
            // 判断匹配大小写
            if(isCase) { 
                // 匹配第一个出现的索引
                index = text.IndexOf(findStr, findNextI);
            } else {
                // 匹配第一个出现的索引
                index = Strings.InStr(findNextI+1,text,findStr,CompareMethod.Text) -1;
            }
            return index;
        }
        /// <summary>
        /// 查询是否到达末尾的判断方法
        /// </summary>
        /// <returns></returns>
        private Boolean isFindEndMet() {
           // 判断当前是否为向上
           if(isUpOrDown == 0) {
              // 判断索引是否小于0或者向上搜索是否到达末尾
                if(findIndex - 1 < 0 || isFindUpEnd) {
                    // 判断到达末尾继续是否选中
                    if(isEndContinue) { 
                        findIndex = findIndexArry.Length;
                        isFindUpEnd = false;
                        return false;
                    } else {
                        // 将向上搜索到达末尾置为true
                        isFindUpEnd = true;
                        return true;
                    }
                } else {
                    return false;
                }    
           } else {
                if(findIndex + 1 > findIndexArry.Length-1 || isFindDownEnd) {
                    // 判断到达末尾继续是否选中
                    if(isEndContinue) { 
                        findIndex = -1;
                        isFindDownEnd = false;
                        return false;
                    } else {
                        // 将向下搜索到达末尾置为true
                        isFindDownEnd = true;
                        return true;
                    }
                } else { 
                    return false;
                }    
           }
           
        }

        /// <summary>
        /// 搜索到达末尾执行的方法
        /// </summary>
        private void findEndMet() {
            // 判断是否按下的是查找按钮
            if(isFindOrRep == 0) {
                // 判断到达末尾继续是否选中
                if (isEndContinue){
                    // 将搜索字符串出现的次数置为0
                    findIndex = 0;
                    if (isUpOrDown == 0) { 
                        isFindUpEnd = false;
                    } if (isUpOrDown == 1) { 
                        isFindDownEnd = false;
                    }
                } else{
                    if(isFindUpEnd && isUpOrDown == 0) {
                        MessageBox.Show("向上搜索已经到达末尾");
                    }
                    if(isFindDownEnd && isUpOrDown == 1) { 
                        MessageBox.Show("向下搜索已经到达末尾");
                    }
                }
            }
        }

        /// <summary>
        /// 查询前的验证方法
        /// </summary>
        private Boolean findCheckMet() {
            if(this.查找内容T.TextLength.Equals(0)) {
                MessageBox.Show("请输入查找内容");
                return false;
            }
            if(this.textBox.TextLength.Equals(0)) {
                MessageBox.Show("要查找的文本框内容为空");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 替换前的验证方法
        /// </summary>
        private Boolean repCheckMet() {
            if(this.查找内容T.TextLength.Equals(0)) {
                MessageBox.Show("请输入查找内容");
                return false;
            }
            if(this.textBox.TextLength.Equals(0)) {
                MessageBox.Show("要查找的文本框内容为空");
                return false;
            }
            if(isFindOrRep == 1 && this.选定内容R.Checked) { 
                MessageBox.Show("无法使用选定内容下的替换功能,请使用全部替换");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 替换的执行方法
        /// </summary>
        private void repMet() {
            // 执行验证
            if(!repCheckMet()) return;
            // 判断是否已经到达末尾
            if(isRepEndMet()) return;
            // 当前文档
            if(isTextOrSelectT == 0) {
                // 判断当前选中内容是否等于要替换的内容
                if(!findChars.ToLower().Equals(textBox.SelectedText.ToLower())) findMet();
                // 替换文本
                int seletI = textBox.SelectionStart;
                textBox.SelectedText = repChars;
                textBox.Select(seletI, repChars.Length);
                // 重新赋值文本
                text = textBox.Text;
                // 替换后刷新索引数组
                repResetFindIndexArr(seletI);
            } 
        }

        /// <summary>
        /// 替换后刷新索引数组
        /// </summary>
        private void repResetFindIndexArr(int selectIndex) {
            int arrCout = findIndexArry.Length;
            List<int> l = findOutIntArrMet(text, findChars, isCass);
            findIndexArry = l.ToArray();
            if(isUpOrDown != 0) { 
                findIndex = findIndex - (arrCout - l.Count);
            } 
            
        }
        /// <summary>
        /// 判断替换是否已经到达末尾
        /// </summary>
        /// <returns></returns>
        private Boolean isRepEndMet() {
            int index = textBox.SelectionLength != 0?textBox.SelectionStart :0;
            // 判断是否为向下
            if(isUpOrDown == 1) { 
                // 判断匹配是否已经到达末尾
                int i = text.IndexOf(findChars,index+findChars.Length);
                if(i==-1 && !isEndContinue) { 
                    isRepDownEnd = true;
                    isFindDownEnd = true;

                } else { 
                    isRepDownEnd = false;
                    isFindDownEnd = false;
                }         
            } else { 
                // 判断匹配是否已经到达末尾
                int i = text.Substring(0,index+findChars.Length).LastIndexOf(findChars);
                if(i==-1 && !isEndContinue) { 
                    isRepDownEnd = true;
                    isFindUpEnd = true;
                } else { 
                    isRepDownEnd = false;
                    isFindUpEnd = false;
                }   
            } 
            if(isRepDownEnd) {
                textBox.SelectedText = repChars;
                textBox.Select(index,repChars.Length);
                if(isUpOrDown == 0) MessageBox.Show("向上替换已到达末尾");
                if(isUpOrDown == 1) MessageBox.Show("向下替换已到达末尾");
            }
            return isRepDownEnd;
        }



        /// <summary>
        /// 全部替换的执行方法
        /// </summary>
        private void repAllMet() {
            // 执行验证
            if(!repCheckMet()) return;
            string tempStr  = "";
            // 当前文档
            if(isTextOrSelectT == 0) { 
              tempStr = textBox.Text;
              // 是否区分大小写
              textBox.Text = StringUtils.ReplaceCaseText(textBox.Text,findChars,repChars,isCass);
            }
            //选中内容
            if(isTextOrSelectT == 1) {
              tempStr = textBox.SelectedText;
              // 记录文本框起始选中位置
              int index = textBox.SelectionStart;
              // 是否区分大小写
              string s = StringUtils.ReplaceCaseText(textBox.SelectedText,findChars,repChars,isCass);
              // 将文本框的选中内容替换为替换后的内容
              textBox.SelectedText = s;
              // 将替换后的内容选中
              if(s.Length > 0){ 
                  textBox.Select(index, s.Length);
              } else { 
                  textBox.Select(index, tempStr.Length);
              } 
            }
            // 成功后弹出提示消息
            repMessShow(tempStr, findChars, isCass);
        }
        /// <summary>
        /// 替换成功弹出提示消息
        /// </summary>
        /// <returns></returns>
        private void repMessShow(string text, string findChars, Boolean isCase) { 
            string tempText = text;
            int count =  StringUtils.GetIndexOfAllCount(tempText, findChars, isCase);
            //MessageBox.Show("已成功替换 "+count+" 处");
        }
        /// <summary>
        /// 设置当前索引和总共的索引
        /// </summary>
        private void setCurrentLab() {
            string currentStr = "";
            string thisText = this.Text.Split('_')[0];
            if(text.Length ==0 || 查找内容T.TextLength == 0) { 
                currentStr = "(无结果)";
            } else { 
                // 将当前索引和找到的全部索引赋值给标签
                currentStr = "("+(findIndex+1) + "/" + findIndexArry.Length + ")";
            }            
            this.Text = thisText + "_"+ currentStr;
        }
        // 查找按钮的点击事件
        private void 查找B_Click(object sender, EventArgs e)
        {
            // 初始化数据
            if(!initData()) return;
            // 判断当前方式不是查询就重置搜索
            if(isFindOrRep == 2) isResetFindMet = true;
            // 当前为查询方式
            isFindOrRep = 0;
            // 赋值要查找的字符串
            if(!this.查找内容T.Text.Equals(findChars)) {
                isResetFindMet = true;
            }
            findChars = this.查找内容T.Text;
            // 记录到查找历史中
            addFindHistorical(this.查找内容T.Text);
            // 执行查找方法
            findMet();
            // 跳到选中
            textBox.ScrollToCaret();
            // 设置当前索引和总共的索引
            setCurrentLab();
        }
        // 替换按钮的点击事件
        private void 替换B_Click(object sender, EventArgs e)
        {
            if(!initData()) return;
            // 执行替换方法
            repClickMet(1);
        }
        // 全部替换按钮的点击事件
        private void 全部替换B_Click(object sender, EventArgs e)
        {
            if(!initData()) return;
            // 执行替换方法
            repClickMet(2);
        }
        /// <summary>
        /// 替换按钮的点击事件执行方法
        /// </summary>
        /// <param name="findOrRep"></param>
        private void repClickMet(int findOrRep) { 
            // 当前为全部替换方式
            isFindOrRep = findOrRep;
            // 赋值要查找的字符串
            findChars = this.查找内容T.Text;
            // 赋值要替换的字符串
            repChars = this.替换内容T.Text;
            // 记录到替换历史中
            addRepHistorical(this.替换内容T.Text);
            // 判断执行替换的方法
            if(findOrRep.Equals(1)) {
                // 替换
                repMet();
            } 
            if(findOrRep.Equals(2)) {
                // 全部替换
                repAllMet();
            }
            
            // 设置当前索引和总共的索引
            setCurrentLab();
        }
        // 关闭按钮的点击事件
        private void 关闭B_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // 向下单选按钮选项改变事件
        private void 向下R_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if(rb.Checked){
                // 向下
                isUpOrDown = 1;
                // 点击了向下按钮
                isfindDownBut = true;
            }
        }
        // 向上单选按钮选项改变事件
        private void 向上R_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if(rb.Checked) {
                // 向上
                isUpOrDown = 0;
                // 点击了向上按钮
                isfindUpBut = true;
            } 
        }
        // 当前文档单选按钮选项改变事件
        private void 当前文档R_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if(rb.Checked) {
                isTextOrSelectT = 0;
                text = textBox.Text;
                // 重置搜索
                isResetFindMet = true;
            }
        }
        // 选定内容当前文档单选按钮选项改变事件
        private void 选定内容R_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if(rb.Checked) {
                if(textBox.SelectionLength.Equals(0)) {
                    MessageBox.Show("选中内容为空");
                    this.当前文档R.Checked = true;
                    return;
                }
                isTextOrSelectT = 1;
                selectStatrtI = textBox.SelectionStart;
                text = textBox.SelectedText;
                // 重置搜索
                isResetFindMet = true;
            }
        }
        // 到达末尾重新开始复选框选项改变事件
        private void 到达末尾C_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            // 到达末尾重新开始
            isEndContinue = cb.Checked;
        }
        // 区分大小写复选框选项改变事件
        private void 区分大小写C_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            // 区分大小写
            isCass = cb.Checked;
            // 重置搜索
            isResetFindMet = true;
        }
        // 窗体获得焦点事件
        private void FindAndReplace_Activated(object sender, EventArgs e)
        {
            // 判断当文本框的所选内容长度为0时选定内容单选不开启
            this.选定内容R.Enabled = (textBox.SelectionLength != 0);
            // 判断文本框是否被修改过
            isTextChangMet();
        }
        // 窗口关闭前发送
        private void FindAndReplace_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 清除单例工厂中的本窗体
            // PubCacheArea.FormCache.setSingletonFactory(this.Name, null);
        }
        // 文本框鼠标移入并按下按钮事件
        private void TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox textB = (TextBox)sender;
            textB.ContextMenuStrip = textRightMenu;
        }
        // 查找替换文本框内容改变事件
        private void TextBox_TextChanged(object sender, EventArgs e) {
            TextBox t = (TextBox)sender;
            // 判断是否输入了换行符
            isChangLine(t);
        }
        // 文本框获得焦点事件
        private void TextBox_Enter(object sender, EventArgs e) {
            TextBox t = (TextBox)sender;
            // 判断是否存在换行符
             isChangLine(t);
            // 显示当前文本内容
            //selectTool(t);
        }
        // 文本框失去焦点事件
        private void TextBox_Leave(object sender, EventArgs e) {
            TextBox t = (TextBox)sender;
        }
        /// <summary>
        /// 判断文本框框是否输入了换行符
        /// </summary>
        /// <param name="t"></param>
        private void isChangLine(TextBox t){
            int lineIndex = t.Text.IndexOf(Environment.NewLine);
            int x = this.ClientSize.Width - t.Location.X + 1;
            int y = 0;
            int time = 5000;
            if(lineIndex >= 0 && t.TextLength > 0) {
                // 获得含有换行符的个数
                int lineCount = t.Text.Split(new string[]{Environment.NewLine}, StringSplitOptions.None).Length - 1;
                string tipVal = "当前输入的内容含有 "+lineCount+" 个换行符";
                if(this.查找内容T.Name.Equals(t.Name)) {
                    findToolTip = textTip(t, tipVal, x, y, time);
                }
                if(this.替换内容T.Name.Equals(t.Name)) {
                    repToolTip = textTip(t, tipVal, x, y, time);
                }
            }
            if(findToolTip != null && lineIndex < 0 && this.查找内容T.Name.Equals(t.Name)) { 
                findToolTip = textTip(t, "当前输入的内容含有 0 个换行符", x, y, time);
                findToolTip = null;
            }
            if(repToolTip != null && lineIndex < 0 && this.替换内容T.Name.Equals(t.Name)) { 
                repToolTip = textTip(t, "当前输入的内容含有 0 个换行符", x, y, time);
                repToolTip = null;
            }
        }
        /// <summary>
        ///  显示当前文本框内容的提示
        /// </summary>
        /// <param name="t"></param>
        private void selectTool(TextBox t) {
            if(t.TextLength > 0) {
               ToolTip tip = textTip(t, t.Text,(this.Width - t.Location.X), -t.Location.Y, 5000);
            }
        }
        
        /// <summary>
        /// 获取文本框消息提示
        /// </summary>
        /// <param name="t"></param>
        /// <param name="tipVal"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private ToolTip textTip(TextBox t, string tipVal, int x, int y, int time) { 
            ToolTip toolTip = ControlsUtils.GetControlMessTip(t,tipVal,x,y,time,
                ColorTranslator.FromHtml("#E7D5D5"), Color.Black);
            return toolTip;
        }

        /// <summary>
        /// 追加到查找历史中
        /// </summary>
        /// <param name="text"></param>
        private void addFindHistorical(string text) {
            // 当历史集合长度不为0并且上一个历史不等于当前历史
            if(findHistorical.Count.Equals(0) && text.Length > 0) { 
                findHistorical.Add(text);
            }
            // 确定List中不包含此元素
            if( !findHistorical.Contains(text) && text.Length > 0) {
                findHistorical.Add(text);
            }
        }
        /// <summary>
        /// 追加到替换历史中
        /// </summary>
        /// <param name="text"></param>
        private void addRepHistorical(string text) { 
            // 当历史集合长度不为0并且上一个历史不等于当前历史
            if(repHistorical.Count.Equals(0)) { 
                repHistorical.Add(text);
                return;
            }
            // 确定List中不包含此元素
            if(!repHistorical.Contains(text)) {
                repHistorical.Add(text);
            }
        }
        /// <summary>
        /// 历史记录按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void historyBut_Click(object sender, EventArgs e) {
            Button but = (Button)sender;
            if(查找历史B.Name.Equals(but.Name)) {
                ControlsUtils.GetHistoricalPanel(查找内容T
                    , but.FindForm().Controls
                    , true
                    , findHistorical.ToArray()
                    , 查找内容T.Width+but.Width
                    , 22);
            }
            if(替换历史B.Name.Equals(but.Name)) {
                ControlsUtils.GetHistoricalPanel(替换内容T
                    , but.FindForm().Controls
                    , true
                    , repHistorical.ToArray()
                    , 替换内容T.Width+but.Width
                    , 22);

            }
        }
        

    }
}
