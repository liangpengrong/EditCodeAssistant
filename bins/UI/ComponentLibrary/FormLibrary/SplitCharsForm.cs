using Core.StaticMethod.Method.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UI.ComponentLibrary.ControlLibrary;
using Core.CacheLibrary.OperateCache.DataViewOperateCache;
using Core.CacheLibrary.FormCache;
using Core.DefaultData.DataLibrary;
using UI.ComponentLibrary.MethodLibrary.Util;
using UI.ComponentLibrary.MethodLibrary.Interface;

namespace UI.ComponentLibrary.FormLibrary {
    /// <summary>
    /// 分列窗体
    /// </summary>
    public partial class SplitCharsForm : Form,IComponentInitMode<Form> {

        private RedrawDataTable redrawDataTable;
        /// <summary>
        /// 要操作的文本框
        /// </summary>
        private TextBox textBox;
        /// <summary>
        /// 要操作的字符串
        /// </summary>
        private string text;
        /// <summary>
        /// 单行还是全部的文本
        /// </summary>
        private int isSinglelineOrAll = 1;
        /// <summary>
        /// 字符还是字符索引
        /// </summary>
        private int isCharsOrCharIndex = 1;
        /// <summary>
        /// 保留空列
        /// </summary>
        private bool isNone = false;
        /// <summary>
        /// 是否区分大小写
        /// </summary>
        private bool isSensitive = true;
        /// <summary>
        /// 导出时不包含制表符
        /// </summary>
        private bool excNoHaveTabs = false;
        /// <summary>
        /// 分隔符的集合
        /// </summary>
        private string[] separatorArr = null;
        /// <summary>
        /// 索引分隔符的集合
        /// </summary>
        private int[] indexArr = null;
        // 单元格默认宽度
        // private int cellDefWidth = 100;
        // 单元格默认高度
        private int cellDefHeight = 25;
        // 列头的默认高度
        private int colHeadersHeight = 20;


        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="textBox"></param>
        internal SplitCharsForm() {
            // 赋值要操作的文本框
            this.textBox = ControlsUtils.GetSelectPageTextBox();
            InitializeComponent();
            // 初始化消息提示控件
            initToolTip();
            //DoubleBuffered = true;
        }
        /// <summary>
        /// 打开单例模式下的分割字符串窗口
        /// </summary>
        /// <param name="t">所需文本框</param>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Form initSingleExample(bool isShowTop){
            SplitCharsForm splitChars = null;
            Form form = FormCacheFactory.getSingletonCache(DefaultNameEnum.SPLIT_CHARS_FORM);
            if(form == null || form.IsDisposed || !(form is SplitCharsForm)) { 
                splitChars = this;
                splitChars.Name = EnumUtils.GetDescription(DefaultNameEnum.SPLIT_CHARS_FORM);
                splitChars = FormCacheFactory.ininSingletonForm(splitChars, false);
            } else {
                splitChars = (SplitCharsForm)form;
                splitChars.Activate();
            }
            if(isShowTop) FormCacheFactory.addTopFormCache(splitChars);
            splitChars.Visible = false;
            return splitChars;
        }
        /// <summary>
        /// 打开多例模式下的分割字符串窗口
        /// </summary>
        /// <param name="t">所需文本框</param>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Form initPrototypeExample(bool isShowTop){
            SplitCharsForm splitChars = this;
            splitChars.Name = EnumUtils.GetDescription(DefaultNameEnum.SPLIT_CHARS_FORM)+DateTime.Now.Ticks.ToString();;
            // 加入到顶层窗体集合
            if(isShowTop) FormCacheFactory.addTopFormCache(splitChars);
            // 加入到多例工厂
            FormCacheFactory.addPrototypeCache(DefaultNameEnum.SPLIT_CHARS_FORM, splitChars);
            splitChars.Activate();
            splitChars.Visible = false;
            return splitChars;
        }
        /// <summary>
        /// 判断要操作的字符串
        /// </summary>
        private void isOperatingText() {
            if(textBox != null) { 
                // 判断选中长度是否为0
                if(0.Equals(textBox.SelectionLength)) { 
                    text = textBox.Text;
                } else { 
                    text = textBox.SelectedText;    
                }  
            }
        }
        /// <summary>
        /// 初始化消息提示
        /// </summary>
        private void initToolTip() { 
            string mess1 = "按照固定字符分列数据, 如空格, 逗号等, 其中\\为转义字符";
            string mess2 = "按照固定字符位置分列数据, 如1,2等, 默认从0位开始";
            RedrawPromptMessBut but1 = new RedrawPromptMessBut();
            but1.ButtonMess = mess1;
            but1.Location = new Point(字符_rad.Location.X + 字符_rad.Width + 1, 字符_rad.Location.Y);

            RedrawPromptMessBut but2 = new RedrawPromptMessBut();
            but2.ButtonMess = mess2;
            but2.Location = new Point(字符个数_rad.Location.X + 字符个数_rad.Width + 1, 字符个数_rad.Location.Y);
            分割设置容器.Controls.Add(but1);
            分割设置容器.Controls.Add(but2);
        }
        /// <summary>
        /// 将二维数组的数据放入表格中
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string[][] initRowColuArr(string text) { 
            // 将字符串按照换行符和制表符分割为行列的形式
            string[][] rowColuArr = StringUtils.SplitStrToArray(text
                ,new string[]{ Environment.NewLine }, new string[]{ "\t" }
                , true ,true, true, true);

            return rowColuArr;
        }
        /// <summary>
        /// 读取字符串按照制表符分列换行符分行放入数据表格中
        /// </summary>
        /// <param name="text">要操作的字符串</param>
        private void readTextSetDataView(string[][] rowColuArr) {
            // 判断要操作的字符串不为null和''
            if(rowColuArr == null || 0.Equals(rowColuArr.Length)) return;
            // 要绑定的数据源
            DataTable dt = new DataTable();
            redrawDataTable.SelectAll();
            redrawDataTable.ClearSelection();
            DataRow dr = null;
            // 获取列最大的值
            int maxColu = rowColuArr.Max(x=>x.Length);
            // 生成列标题
            // dt.Columns.Clear();
            for(int i = 0; i < maxColu; i++) {
                dt.Columns.Add((i+1)+"列", typeof(string));
            }
            for(int i = 0; i < rowColuArr.Length; i++) {
                dr = dt.NewRow();
                // 获取当前行的列
                string[] colArr = rowColuArr[i];
                for(int j = 0; j < colArr.Length; j++) { 
                    dr[j] = colArr[j];
                } 
                dt.Rows.Add(dr);
            }
            if (dt.Columns.Count <= 65535) { 
                redrawDataTable.DataSource = dt;
                // 确定窗体的大小
               //  defineFormSize(rowColuArr);
            } else { 
                MessageBox.Show("列超出了最大值");    
            }
        }
        
        /// <summary>
        /// 根据传入的二维数组获取列的最大宽度
        /// </summary>
        /// <param name="rowColuArr"></param>
        /// <returns></returns>
        private int getMaxColumnWidth(string[][] rowColuArr) { 
            List<int> intList = new List<int>();
            foreach(string[] strArr1 in rowColuArr) {
                foreach(string str1 in strArr1) { 
                   intList.Add( (int)CreateGraphics().MeasureString(str1 , redrawDataTable.Font).Width);
                }
            }
            return intList.Max()+10;
        }
        /// <summary>
        /// 验证方法
        /// </summary>
        /// <returns></returns>
        private bool isCheck() {
            // 验证文本框
            if(textBox == null) { 
                MessageBox.Show("无法获取文本框");
                return false;
            }
            if(0.Equals(textBox.TextLength)) { 
                MessageBox.Show("源数据不能为空");
                return false;
            }
            if(1.Equals(isCharsOrCharIndex)) {
                
                // 验证字符输入框
                if(0.Equals(字符_textB.TextLength) && !制表符_chk.Checked && !分号_chk.Checked
                    && !冒号_chk.Checked && !空格_chk.Checked && !逗号_chk.Checked && !点_chk.Checked) { 
                    MessageBox.Show("字符文本框不能为空");
                    return false;
                }
            }
            if(2.Equals(isCharsOrCharIndex)) { 
                // 验证字符个数输入框
                if(0.Equals(字符个数_textB.TextLength)) { 
                    MessageBox.Show("字符个数文本框不能为空");
                    return false;
                }
            }
            
            return true;
        }

        /// <summary>
        /// 确定窗体的大小
        /// </summary>
        /// <param name="rowColuArr">生成表格说使用的数据</param>
        private void defineFormSize(string[][] rowColuArr){
            int cellWidth = redrawDataTable.Rows[0].Cells[0].Size.Width;
            // 数据表格在窗体中所占的宽
            int dataViewW = redrawDataTable.Location.X + redrawDataTable.RowHeadersWidth + (cellWidth * rowColuArr.Max(x=>x.Length));
            // 数据表格在窗体中所占的高
            int dataViewH = redrawDataTable.Location.Y + colHeadersHeight + (cellDefHeight * rowColuArr.Length);
            // 判断工作区的宽
            if(dataViewW > 1000) { 
                Width = MessyUtils.GetResolvingpower()[0] - 100;
            } else if(dataViewW < 600) {
                Width = 600;
            } else{ 
                Width = (Width - ClientSize.Width) + dataViewW + 15;    
            }
            // 判断工作区的高
            if(dataViewH > Screen.PrimaryScreen.Bounds.Height) {
                Height = MessyUtils.GetResolvingpower()[1] - 100;
            } else if(dataViewH < 500) { 
                Height = 500;
            } else{ 
                Height = (Height - ClientSize.Height) + dataViewH + 15;    
            }
            // MinimumSize = new Size(Width, Height);
        }
        /// <summary>
        /// 调节窗体的位置
        /// </summary>
        private void middleForm() {
            Form rootDisplayForm = textBox.FindForm();
            // 根据父窗体居中
            Location = FormUtisl.MiddleForm(this, rootDisplayForm);
            // 获取当前屏幕分辨率
            int[] wh = MessyUtils.GetResolvingpower();
            
            int w = rootDisplayForm.Location.X+rootDisplayForm.Width;
            if(w + Width <= wh[0]) {
                // 设置相对于启动窗体贴右
                Location = new Point(w, Location.Y);
                return;
            }
            if(Width <= rootDisplayForm.Location.X) { 
                w = rootDisplayForm.Location.X - Width;
                Location = new Point(w, Location.Y);
                return;
            }
        }
        /// <summary>
        /// 将文本框中的分隔符添加到分隔符集合中
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string[] addSeparatorArr() {
            List<string> separatorList = new List<string>();
            // 判断复选框
            if(制表符_chk.Checked) separatorList.Add("\t");
            if(分号_chk.Checked) separatorList.Add(";");
            if(冒号_chk.Checked) separatorList.Add(":");
            if(空格_chk.Checked) separatorList.Add(" ");
            if(逗号_chk.Checked) separatorList.Add(",");
            if(点_chk.Checked) separatorList.Add(".");
            // 判断其他文本框
            string splitChars = 字符_textB.Text;
            if(splitChars.IndexOf("\\,") >= 0) separatorList.Add(",");
            if(splitChars.IndexOf("\\\\") >= 0) separatorList.Add("\\");
            // 将字符文本框中的文本添加到分隔符集合中
            if(splitChars != null && !"".Equals(splitChars) && 1.Equals(isCharsOrCharIndex)) {
                string[] tempArr;
                tempArr = splitChars.Split(new string[]{","}, StringSplitOptions.RemoveEmptyEntries);
                foreach(string s in tempArr) {
                    if( !separatorList.Contains(s) && s.Length >0) { 
                        separatorList.Add(s);
                    }
                }
            }
            // 赋值全局变量
            separatorArr = separatorList.ToArray();
            return separatorList.ToArray();
        }
        /// <summary>
        /// 将文本框中的索引分隔符添加到索引分隔符集合中
        /// </summary>
        /// <returns></returns>
        private int[] addIndexArr() {
            try { 
                string splitChars = 字符个数_textB.Text;
                // 将字符串按逗号分割
                string[] tempStrArr = splitChars.Split(new string[]{","}, StringSplitOptions.RemoveEmptyEntries);
                // 将字符串数组转化为int数组并赋值全局变量
                var tempList = new List<int>();
                foreach(string s in tempStrArr) {
                    try { tempList.Add(Convert.ToInt32(s));} catch { }
                   // 将字符串转化为int
                   
                }
                indexArr = tempList.ToArray();
                // 去重数组
                indexArr = indexArr.Distinct().ToArray();
                // 排序数组
                Array.Sort(indexArr);
            } catch { 
                indexArr = null;
                
            }
            return indexArr;
            
        }
        /// <summary>
        /// 字符分列的执行方法
        /// </summary>
        private string[][] charsSplitMethod(){
            // 获取分隔符集合
            addSeparatorArr();
            // 初始化要填充到表格中的数据
            string[][] rowColArr = null;
            // 判断是单行还是全部文本的执行方法
            isSinglelineOrAllMet();
            // 执行分列
            rowColArr = StringUtils.SplitStrToArray(text, new string[]{Environment.NewLine}
                , separatorArr, true, isNone, true, isSensitive);
            return rowColArr;
        }
        /// <summary>
        /// 字符索引分列的执行方法
        /// </summary>
        /// <returns></returns>
        private string[][] charsIndexSplitMethod(){
            // 获取分隔符集合
            addIndexArr();
            // 初始化要填充到表格中的数据
            string[][] rowColArr = null;
            // 判断是单行还是全部文本的执行方法
            isSinglelineOrAllMet();
            // 执行分列
            string[] tempArr = StringUtils.SplitStrToArray(text, new string[]{Environment.NewLine}
            ,isNone, true);
            rowColArr = new string[tempArr.Length][];
            for(int i = 0, len = tempArr.Length; i< len; i++) { 
                string s = tempArr[i];
                string[] tempArr2 = StringUtils.SplitStrToArray(s, indexArr, isNone, false);
                rowColArr[i] = tempArr2;
            }
            return rowColArr;
        }
        /// <summary>
        /// 判断是单行还是全部文本的执行方法
        /// </summary>
        private void isSinglelineOrAllMet() { 
            // 判断是单行还是全部的文本
            if(isSinglelineOrAll == 1) { 
                
            } 
            if(isSinglelineOrAll == 2) { 
               text = text.Replace(Environment.NewLine, "");
            }
        }
        /// <summary>
        /// 总的分列执行方法
        /// </summary>
        private void splitMethod() {
            string[][] rowColArr = null;
            // 判断是否为字符分列方式
            if(1.Equals(isCharsOrCharIndex)) { 
                rowColArr = charsSplitMethod();
            }
            // 判断是否为字符索引分列方式
            if(2.Equals(isCharsOrCharIndex)) { 
                rowColArr = charsIndexSplitMethod();
            }
            // 填入到表格中
            readTextSetDataView(rowColArr);
        }

        /// <summary>
        /// 初始化数据表格配置
        /// </summary>
        private void initDataViewConf() {
            RedrawDataTable dataView = new RedrawDataTable();
            dataView.CellDefaultHeight = cellDefHeight;
            dataView.ColumnHeadDefaultHeight = colHeadersHeight;
            dataView.Location = new Point(操作区容器.Location.X, 操作区容器.Bottom+5);
            dataView.Size = new Size(操作区容器.Width, 选项区容器.Bottom-dataView.Location.Y);
            redrawDataTable = dataView;
            // 记录到状态栏中
            Control c = UIComponentFactory.getSingleControl(DefaultNameEnum.TOOL_START);
            if(c != null && c is RedrawStatusBar) { 
                RedrawStatusBar bar = (RedrawStatusBar)c;
                bar.SetSourceControl(dataView);
            }
            // 加入到容器中
            this.Controls.Add(redrawDataTable);

        }
        /// <summary>
        /// 设置字符文本框中特定字符的背景色
        /// </summary>
        /// <param name="rTextBox"></param>
        private void selectRichColor(RichTextBox rTextBox) {
            int selStart = rTextBox.SelectionStart;
            string richText = rTextBox.Text;
            // 设置选中背景色
            rTextBox.Select(0, richText.Length);
            rTextBox.SelectionBackColor = ColorTranslator.FromHtml("#DB494E");
            rTextBox.SelectionColor = Color.White;
            // 查找逗号的位置
            int[] indexArr = StringUtils.GetCharsIndexOf(richText, ",", true);
            // 判断是否找到了逗号
            if(indexArr != null && indexArr.Length > 0) {
                for (int i = 0, len = indexArr.Length; i < len; i++) {
                    int index = indexArr[i];
                    if (index >= 0 && index < richText.Length) {
                        // 判断逗号前一个字符是否为转义字符
                        if(index > 0 && "\\".Equals(richText.Substring(index -1,1))) { 
                            continue;
                        }
                        rTextBox.Select(index, 1);
                        rTextBox.SelectionBackColor = rTextBox.BackColor;
                        rTextBox.SelectionColor = rTextBox.ForeColor;

                    }
                }
            }
            rTextBox.SelectionStart = selStart;
            rTextBox.SelectionLength = 0;
        }
        /// <summary>
        /// 设置导出按钮
        /// </summary>
        private void setExportCombox() {
            // 实例化导出下拉框
            ComboBox comboBox = new ExportComBox(new ExportComBoxValEnum[]{ExportComBoxValEnum.EXPORT_JAVA_VAL });
            // 绑定点击事件
            comboBox.SelectedIndexChanged += (object sender, EventArgs e) =>{
                ComboBox box = (ComboBox)sender;
                ExportComBoxValEnum val = ExportComBox.stringToEnum(box.SelectedValue.ToString());
                switch(val) {
                    case ExportComBoxValEnum.EXPORT_NEW_PAGE_VAL:
                        DataGridViewUtilMet.exportNewPage(redrawDataTable, excNoHaveTabs);
                    break;
                    case ExportComBoxValEnum.EXPORT_THIS_PAGE_VAL:
                        DataGridViewUtilMet.exportThisPage(redrawDataTable, excNoHaveTabs);
                    break;
                    case ExportComBoxValEnum.EXPORT_NOTEBOOK_VAL:
                        DataGridViewUtilMet.exportNotepad(redrawDataTable, excNoHaveTabs);
                    break;
                    case ExportComBoxValEnum.EXPORT_EXCEL_VAL:
                        MessageBox.Show("该功能尚未完成");
                        // DataGridViewUtilMet.exportExcel(mainDataGridView, excNoHaveTabs);
                    break;
                }
            };
            // 加入到容器中
            表格内容_label.Location = new Point(表格内容_label.Location.X, (操作区容器.Height-表格内容_label.Height)/2+4);
            comboBox.Location = new Point(表格内容_label.Right+5, (操作区容器.Height-comboBox.Height)/2+2);
            操作区容器.Controls.Add(comboBox);
            comboBox.BringToFront();
        }
        /// <summary>
        /// 获取表格中选定单元格的数据
        /// </summary>
        /// <param name="isSelectAll">是否全在全部</param>
        /// <returns></returns>
        private string getDatatabelSelText(bool isSelectAll) { 
            string tableText = "";
            // 选定的单元格集合
            DataGridViewSelectedCellCollection selCell = redrawDataTable.SelectedCells;
            if(isSelectAll) redrawDataTable.SelectAll();
            tableText = redrawDataTable.GetClipboardContent().GetText();
            // 还原
            if(isSelectAll) { 
                redrawDataTable.ClearSelection();
                foreach(DataGridViewCell cell in selCell) { 
                    cell.Selected = true;
                }
            }
            return tableText;
        }

        // 窗体加载事件
        private void SplitOrAddChars_Load(object sender, EventArgs e) {
            // 读取窗体的默认配置
            formDefConfig();
            // 加载数据表格配置
            initDataViewConf();
            // 判断要操作的字符串
            isOperatingText();
            // 数据表格生成数据
            readTextSetDataView(initRowColuArr(text));
            // 设置导出按钮
            setExportCombox();
        }
        /// <summary>
        /// 窗体默认配置
        /// </summary>
        private void formDefConfig() { 
            // 设置图标
            this.Icon = MessyUtils.IamgeToIcon(Core.ImageResource.分割,true);
            this.AutoScaleMode = AutoScaleMode.None;
            // 调节窗口位置
            this.Location = FormUtisl.MiddleForm(this);
        }
        // 单选按钮选项改变事件
        private void Radio_CheckedChanged(object sender, EventArgs e) {
            RadioButton radBut = (RadioButton)sender;
            if(radBut.Equals(字符个数_rad)) {
                // 赋值变量
                if(radBut.Checked) isCharsOrCharIndex = 2;
                // 一些关联控件的设置
                字符个数_textB.Focus();
                制表符_chk.Enabled = !radBut.Checked;
                分号_chk.Enabled = !radBut.Checked;
                冒号_chk.Enabled = !radBut.Checked;
                空格_chk.Enabled = !radBut.Checked;
                逗号_chk.Enabled = !radBut.Checked;
                点_chk.Enabled = !radBut.Checked;
                字符_textB.Enabled = !radBut.Checked;
                不区分大小写_chk.Enabled = !radBut.Checked;
                字符个数_textB.Enabled = radBut.Checked;
            }
            if(radBut.Equals(字符_rad)) {
                if(radBut.Checked) isCharsOrCharIndex = 1;
                字符_textB.Focus();
                字符个数_textB.Enabled = !radBut.Checked;
            }
            if(radBut.Equals(单个行_rad)) { 
                isSinglelineOrAll = 1;    
            }
            if(radBut.Equals(整个文本_rad)) { 
                isSinglelineOrAll = 2;    
            }
        }
        // 复选框选项改变事件
        private void Chk_CheckedChanged(object sender, EventArgs e) {
            CheckBox chk = (CheckBox)sender;
            if(chk.Equals(保留空列_chk)) { 
                isNone = chk.Checked;
            }
            if(chk.Equals(不区分大小写_chk)) { 
                isSensitive = !chk.Checked;
            }
            if(chk.Equals(不包含制表符_chk)) {
                excNoHaveTabs = chk.Checked;
            }
        }
        // 分列按钮点击事件
        private void 分列_but_Click(object sender, EventArgs e) {
            // 赋值要操作的文本框
            textBox = ControlsUtils.GetSelectPageTextBox();
            // 清空缓存
            DataViewCache.removeCacheFactory(redrawDataTable.Name);
            // 执行验证
            if(!isCheck()) return;
            // 判断要操作的字符串
            isOperatingText();
            // 执行分列
            splitMethod();
        }
        private void 关闭_but_Click(object sender, EventArgs e) {
            Close();
        }
        // 字符文本框文本改变事件
        private void 字符_textB_TextChanged(object sender, EventArgs e) {
            RichTextBox richT = (RichTextBox)sender;
            // 设置特定文本颜色
            // selectRichColor(richT);
        }
    }
}
