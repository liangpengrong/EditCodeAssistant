using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using PubMethodLibrary;
using System.Windows.Forms;

namespace PubControlLibrary {
    public partial class SplitCharsForm : Form {
        /// <summary>
        /// 存放数据的数据表格
        /// </summary>
        private DataGridView mainDataGridView;
        /// <summary>
        /// 存放当前鼠标所在单元格
        /// </summary>
        private DataGridViewCell mouseCell = null;

        /// <summary>
        /// 要操作的文本框
        /// </summary>
        private TextBox textBox;
        /// <summary>
        /// 要操作的字符串
        /// </summary>
        private String text;
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
        private Boolean isNone = false;
        /// <summary>
        /// 是否区分大小写
        /// </summary>
        private Boolean isSensitive = true;
        /// <summary>
        /// 导出时不包含制表符
        /// </summary>
        private Boolean excNoHaveTabs = false;
        /// <summary>
        /// 分隔符的集合
        /// </summary>
        private String[] separatorArr = null;
        /// <summary>
        /// 索引分隔符的集合
        /// </summary>
        private int[] indexArr = null;
        // 单元格默认宽度
        private int cellDefWidth = 100;
        // 单元格默认高度
        private int cellDefHeight = 25;
        // 列头的默认高度
        private int colHeadersHeight = 20;


        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="textBox"></param>
        public SplitCharsForm(TextBox textBox) {
            // 赋值要操作的文本框
            this.textBox = textBox;
            InitializeComponent();
            // 初始化消息提示控件
            initToolTip();
            //DoubleBuffered = true;
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
            Button but1 = PromptMessage.getMessBut(字符_rad.Name+"mess_but", mess1);
            but1.Location = new Point(字符_rad.Location.X + 字符_rad.Width + 1, 字符_rad.Location.Y);

            Button but2 = PromptMessage.getMessBut(字符个数_rad.Name+""+"mess_but", mess2);
            but2.Location = new Point(字符个数_rad.Location.X + 字符个数_rad.Width + 1, 字符个数_rad.Location.Y);
            分列设置容器.Controls.Add(but1);
            分列设置容器.Controls.Add(but2);
        }
        /// <summary>
        /// 将二维数组的数据放入表格中
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private String[][] initRowColuArr(String text) { 
            // 将字符串按照换行符和制表符分割为行列的形式
            String[][] rowColuArr = PubMethodLibrary.StringUtilsMet.splitStrToArr(text
                ,new String[]{ Environment.NewLine }, new String[]{ "\t" }
                , true ,true, true, true);

            return rowColuArr;
        }
        /// <summary>
        /// 读取字符串按照制表符分列换行符分行放入数据表格中
        /// </summary>
        /// <param name="text">要操作的字符串</param>
        private void readTextSetDataView(String[][] rowColuArr) {
            // 判断要操作的字符串不为null和''
            if(rowColuArr == null || 0.Equals(rowColuArr.Length)) return;
            // 清空表格的行
            mainDataGridView.Rows.Clear();
            // 清空表格的列
            mainDataGridView.Columns.Clear();
            // 获取列最大的值
            int maxColu = rowColuArr.Max(x=>x.Length);
            // 创建列头
            for (int i = 0; i < maxColu; i++) {
                mainDataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
                // 添加列头并且返回添加的索引
                int index = mainDataGridView.Columns.Add("列" + (i + 1), "列" + (i + 1));
                // 不包含排序
                mainDataGridView.Columns[index].SortMode = DataGridViewColumnSortMode.NotSortable;
                // 设置列的宽度
                // mainDataGridView.Columns[index].Width = getMaxColumnWidth(rowColuArr);
                mainDataGridView.Columns[index].Width = cellDefWidth;
            }
            // 先生成空行
            mainDataGridView.Rows.Add(rowColuArr.Length > 1 ? rowColuArr.Length : 1);
            // 判断生成空行最大的数对应的宽度
            int rowHandW = (int)CreateGraphics().MeasureString((rowColuArr.Length - 1).ToString() , mainDataGridView.Font).Width;
            // 行头宽度
            mainDataGridView.RowHeadersWidth = rowHandW + 40;
            // 往单元格里放数据
            for (int i = 0, rowLen = rowColuArr.Length; i < rowLen; i++) {
                // 获取当前行的列
                String[] colArr = rowColuArr[i];
                //将序列赋值给行头
                mainDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                // 循环添加当前行的列
                for (int j = 0, colLen = colArr.Length; j < colLen; j++)
                {
                    String colVal  = colArr[j];
                    //将集合某个对应元素内容赋值给单元格
                    mainDataGridView.Rows[i].Cells[j].Value = colVal;
                    //将集合某个对应元素内容赋值给单元格提示
                    mainDataGridView.Rows[i].Cells[j].ToolTipText = colVal;
                    // 设置单元格的宽
                    //if(i == 0) mainDataGridView.Columns[j].Width = cellDefWidth;
                }
                // 设置单元格的高
                mainDataGridView.Rows[i].Height = cellDefHeight;
            }
            // 确定窗体的大小
            defineFormSize(rowColuArr);
        }
        
        /// <summary>
        /// 根据传入的二维数组获取列的最大宽度
        /// </summary>
        /// <param name="rowColuArr"></param>
        /// <returns></returns>
        private int getMaxColumnWidth(String[][] rowColuArr) { 
            List<int> intList = new List<int>();
            foreach(String[] strArr1 in rowColuArr) {
                foreach(String str1 in strArr1) { 
                   intList.Add( (int)CreateGraphics().MeasureString(str1 , mainDataGridView.Font).Width);
                }
            }
            return intList.Max()+10;
        }
        /// <summary>
        /// 验证方法
        /// </summary>
        /// <returns></returns>
        private Boolean isCheck() {
            // 验证文本框
            if(textBox == null) { 
                MessageBox.Show("无法获取文本框");
                return false;
            }
            if(0.Equals(textBox.TextLength)) { 
                MessageBox.Show("文本框不能为空");
                return false;
            }
            if(1.Equals(isCharsOrCharIndex)) { 
                // 验证字符输入框
                if(0.Equals(字符_textB.TextLength) && !制表符_chk.Checked && !分号_chk.Checked
                    && !冒号_chk.Checked && !空格_chk.Checked) { 
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
        private void defineFormSize(String[][] rowColuArr){
            int cellWidth = mainDataGridView.Rows[0].Cells[0].Size.Width;
            // 数据表格在窗体中所占的宽
            int dataViewW = mainDataGridView.Location.X + mainDataGridView.RowHeadersWidth + (cellWidth * rowColuArr.Max(x=>x.Length));
            // 数据表格在窗体中所占的高
            int dataViewH = mainDataGridView.Location.Y + colHeadersHeight + (cellDefHeight * rowColuArr.Length);
            // 判断工作区的宽
            if(dataViewW > 1000) { 
                Width = MessyUtilsMet.getResolvingpower()[0] - 100;
            } else if(dataViewW < 600) {
                Width = 600;
            } else{ 
                Width = (Width - ClientSize.Width) + dataViewW + 15;    
            }
            // 判断工作区的高
            if(dataViewH > Screen.PrimaryScreen.Bounds.Height) {
                Height = MessyUtilsMet.getResolvingpower()[1] - 100;
            } else if(dataViewH < 500) { 
                Height = 500;
            } else{ 
                Height = (Height - ClientSize.Height) + dataViewH + 15;    
            }
            MinimumSize = new Size(Width, Height);
        }
        /// <summary>
        /// 调节窗体的位置
        /// </summary>
        private void middleForm() {
            Form rootDisplayForm = textBox.FindForm();
            // 根据父窗体居中
            Location = FormUtislMet.middleForm(this, rootDisplayForm);
            // 获取当前屏幕分辨率
            int[] wh = MessyUtilsMet.getResolvingpower();
            
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
        private String[] addSeparatorArr() {
            List<String> separatorList = new List<string>();
            // 判断复选框
            if(制表符_chk.Checked) separatorList.Add("\t");
            if(分号_chk.Checked) separatorList.Add(";");
            if(冒号_chk.Checked) separatorList.Add(":");
            if(空格_chk.Checked) separatorList.Add(" ");
            // 判断其他文本框
            String splitChars = 字符_textB.Text;
            if(splitChars.IndexOf("\\,") >= 0) separatorList.Add(",");
            if(splitChars.IndexOf("\\\\") >= 0) separatorList.Add("\\");
            // 将字符文本框中的文本添加到分隔符集合中
            if(splitChars != null && !"".Equals(splitChars) && 1.Equals(isCharsOrCharIndex)) {
                String[] tempArr;
                tempArr = splitChars.Split(new String[]{","}, StringSplitOptions.RemoveEmptyEntries);
                foreach(String s in tempArr) {
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
                String splitChars = 字符个数_textB.Text;
                // 将字符串按逗号分割
                String[] tempStrArr = splitChars.Split(new String[]{","}, StringSplitOptions.RemoveEmptyEntries);
                // 将字符串数组转化为int数组并赋值全局变量
                var tempList = new List<int>();
                foreach(String s in tempStrArr) {
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
        private String[][] charsSplitMethod(){
            // 获取分隔符集合
            addSeparatorArr();
            // 初始化要填充到表格中的数据
            String[][] rowColArr = null;
            // 判断是单行还是全部文本的执行方法
            isSinglelineOrAllMet();
            // 执行分列
            rowColArr = StringUtilsMet.splitStrToArr(text, new String[]{Environment.NewLine}
                , separatorArr, true, isNone, true, isSensitive);
            return rowColArr;
        }
        /// <summary>
        /// 字符索引分列的执行方法
        /// </summary>
        /// <returns></returns>
        private String[][] charsIndexSplitMethod(){
            // 获取分隔符集合
            addIndexArr();
            // 初始化要填充到表格中的数据
            String[][] rowColArr = null;
            // 判断是单行还是全部文本的执行方法
            isSinglelineOrAllMet();
            // 执行分列
            String[] tempArr = StringUtilsMet.splitStrToArr(text, new String[]{Environment.NewLine}
            ,isNone, true);
            rowColArr = new string[tempArr.Length][];
            for(int i = 0, len = tempArr.Length; i< len; i++) { 
                String s = tempArr[i];
                String[] tempArr2 = StringUtilsMet.splitStrToArr(s, indexArr, isNone, false);
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
            // 判断是否为字符分列方式
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
            MyDataTable dataTable = new MyDataTable();
            dataTable.cellDefHeight = cellDefHeight;
            dataTable.cellDefWidth = cellDefWidth;
            dataTable.colHeadersHeight = colHeadersHeight;
            Point point = 数据表格.Location;
            Size size = 数据表格.Size;
            string name = 数据表格.Name;

            DataGridView dataView = dataTable.数据表格;
            dataView.Name = name;
            dataView.Location = point;
            dataView.Size = 数据表格.Size;
            // 设置单元格移入事件
            dataView.CellMouseEnter += (object sender, DataGridViewCellEventArgs e) =>{ 
                DataGridView data = (DataGridView)sender;
                if(e.ColumnIndex != -1 && e.RowIndex != -1) { 
                    mouseCell = data.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
            };
            数据表格.Dispose();
            Controls.Add(dataView);
            mainDataGridView = dataView;
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
            int[] indexArr = StringUtilsMet.getCharsIndexOf(richText, ",", true);
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
            ExportComBox exportComBox = new ExportComBox();
            ComboBox comboBox = exportComBox.export_combox;
            // 绑定点击事件
            comboBox.SelectedIndexChanged += (object sender, EventArgs e) =>{
                ComboBox box = (ComboBox)sender;
                int val = int.Parse(box.SelectedValue.ToString());
                switch(val) {
                    case 0:
                        DataGridViewUtilMet.exportText(mainDataGridView, textBox, excNoHaveTabs);
                        break;
                    case 1:
                        DataGridViewUtilMet.exportNotepad(mainDataGridView, excNoHaveTabs);
                        break;
                    case 2:
                        DataGridViewUtilMet.exportExcel(mainDataGridView, excNoHaveTabs);
                        break;
                }
            };
            // 加入到容器中
            comboBox.Location = new Point(5,18);
            操作区容器.Controls.Add(comboBox);
        }
        /// <summary>
        /// 获取表格中选定单元格的数据
        /// </summary>
        /// <param name="isSelectAll">是否全在全部</param>
        /// <returns></returns>
        private string getDatatabelSelText(bool isSelectAll) { 
            string tableText = "";
            // 选定的单元格集合
            DataGridViewSelectedCellCollection selCell = mainDataGridView.SelectedCells;
            if(isSelectAll) mainDataGridView.SelectAll();
            tableText = mainDataGridView.GetClipboardContent().GetText();
            // 还原
            if(isSelectAll) { 
                mainDataGridView.ClearSelection();
                foreach(DataGridViewCell cell in selCell) { 
                    cell.Selected = true;
                }
            }
            return tableText;
        }

        // 窗体加载事件
        private void SplitOrAddChars_Load(object sender, EventArgs e) {
            // 设置图标
            Icon = MessyUtilsMet.IamgeToIcon(StaticDataLibrary.Image.分列,true);
            // 加载数据表格配置
            initDataViewConf();
            // 判断要操作的字符串
            isOperatingText();
            // 数据表格生成数据
            readTextSetDataView(initRowColuArr(text));
            // 设置导出按钮
            setExportCombox();
            // 调节窗口位置
            // middleForm();
            Location = FormUtislMet.middleForm(this);
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
            // 执行验证
            if( !isCheck()) return;
            // 判断要操作的字符串
            isOperatingText();
            // 执行分列
            splitMethod();
        }
        // 窗体关闭事件
        private void SplitCharsForm_FormClosed(object sender, FormClosedEventArgs e) {
            // 清除单例工厂中的本窗体
            // PubCacheArea.FormCache.setSingletonFactory(Name, null);
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
