using Core.CacheLibrary.ControlCache;
using Core.CacheLibrary.FormCache;
using Core.CacheLibrary.OperateCache.TextBoxOperateCache;
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
using UI.ComponentLibrary.ControlLibrary;
using UI.ComponentLibrary.ControlMethod;

namespace UI.ComponentLibrary.FormLibrary {
    public partial class FieldToJavaEntity : Form {
        // 换行符
        private static string LINE = Environment.NewLine;
        // 一个Tab字符
        private static string TAB_STR = "    ";
        // 最终要根据此数组生成get/set 0位-字段类型 1位-字段名称 2位-注释
        private List<string[]> dataStrs = new List<string[]>();
        // 属于手动输入的数据表格
        private DataGridView inputDataGridView = null;
        // 类命
        private string classNameStr = "";
        // 包名
        private string packageNameStr = "";
        // 生成get/set
        private bool isGetAndSet = true;
        // 生成构造器
        private bool isConstructor = true;
        // 生成注释
        private bool isComment = true;
        // 生成字段注释
        private bool isFieldComment = true;
        // 生成get/set注释
        private bool isGetSetComment = true;
        /// <summary>
        /// 生成构造器注释
        /// </summary>
        private bool isConstrComment = true;
        // get/set生成类型 0-全部生成 1-只生成get 2-只生成set
        private int getAndSetType = 0;
        // get/set生成类型 0-get/set对中 1-先get后set 2-先set后get
        private int getAndSetRule = 0;
        // 生成深拷贝方法
        private bool isDeepClone = true;
        // 生成ToString方法
        private bool isToString = true;
        // 是否序列化
        private bool isSerialization = true;
        private string columnNameStr = "字段名称";
        private string columnTypeStr = "字段类型";
        private string columnAnnotateStr = "字段注释";
        private FieldToJavaEntity() {
            InitializeComponent();
        }
        // 验证参数
        private bool checkProperty() { 
            if(classNameStr.Length == 0) { 
                MessageBox.Show("类名不能为空");
                return false;
            }else if(packageNameStr.Length == 0) { 
                MessageBox.Show("包名不能为空");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 实例化字段转实体类窗体的多例窗体
        /// </summary>
        /// <param name="isShowTop"></param>
        /// <returns></returns>
        public static FieldToJavaEntity initPrototypeFieldToJavaEntity(bool isShowTop) { 
            FieldToJavaEntity fieldToJavaEntity = new FieldToJavaEntity();
            fieldToJavaEntity.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.FIELD_TO_JAVA_ENTITY)+DateTime.Now.Ticks.ToString();
            // 加入到顶层窗体集合
            if(isShowTop) FormCacheFactory.addTopFormCahce(fieldToJavaEntity);
            // 加入到多例工厂
            FormCacheFactory.addPrototypeCache(DefaultNameEnum.FIELD_TO_JAVA_ENTITY, fieldToJavaEntity);
            fieldToJavaEntity.Show();
            return fieldToJavaEntity;
        }
        // 窗体加载事件
        private void FieldToJavaEntity_Load(object sender, EventArgs e) {
            this.ShowIcon = false;
            // 加载数据表格配置
            initInputDataViewConf();
            // 数据表格生成数据
            initDataViewStr(inputDataGridView);
            输入_生成到_comB.SelectedIndex = 0;
            // 生成消息提示
            initToolTip();
            // 调节窗口位置
            Location = FormUtislMet.middleForm(this);
        }
        /// <summary>
        /// 初始化手动输入的数据表格配置
        /// </summary>
        private void initInputDataViewConf() {
            DataGridView dataView = DataTableTemplate.GetDataGridViewTempl(27, 30, Color.Empty, Color.Empty);
            dataView.Location = new Point(1, 输入_生成到_comB.Bottom + 输入_生成到_comB.Location.Y);
            inputDataGridView = dataView;
            输入_page.Controls.Add(inputDataGridView);
            inputDataGridView.BringToFront();
            inputDataGridView.Width = 输入_page.ClientSize.Width - 4;
            inputDataGridView.Height = 输入_page.ClientSize.Height - (inputDataGridView.Location.Y+1);
            inputDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            inputDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // inputDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            inputDataGridView.AllowUserToResizeColumns = false;
            // inputDataGridView.AllowUserToResizeRows = false;
            inputDataGridView.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
        }

        /// <summary>
        /// 初始化消息提示
        /// </summary>
        private void initToolTip() { 
            Dictionary<Control, string> dic = new Dictionary<Control, string>();
            dic.Add(生成深拷贝方法_check, "只对类型为基础类型的包装类有效");
            foreach (KeyValuePair<Control, string> kvp in dic) { 
                Button but1 = PromptMessage.getMessBut(kvp.Key.Name+"mess_but", kvp.Value);
                but1.Location = new Point(kvp.Key.Right + 2, kvp.Key.Location.Y);
                kvp.Key.Parent.Controls.Add(but1);
            }
        }
        /// <summary>
        /// 初始化表格内容
        /// </summary>
        /// <param name="view"></param>
        private void initDataViewStr(DataGridView view) { 
            if(view == null) return;
            // 要绑定的数据源
            DataTable dt = new DataTable();
            DataRow dr = null;
            // 列标题数组
            string[] headTexts = new string[] { columnNameStr, columnTypeStr, columnAnnotateStr};
            // 生成列标题
            foreach (string s in headTexts) { 
                dt.Columns.Add(s, typeof(string));
            }
            // 测试数据
            //List<string[]> aaa = new List<string[]>();
            //aaa.Add(new string[] { "id", "int", "自增主键" });
            //aaa.Add(new string[] { "code", "varchar", "平台_模块_功能_短信/邮件" });
            //aaa.Add(new string[] { "desc", "varchar", "模板描述" });
            //aaa.Add(new string[] { "title", "varchar", "标题" });
            //aaa.Add(new string[] { "template", "varchar", "模板内容" });
            //aaa.Add(new string[] { "is_english", "varchar", "0-中文模板，1-英文模板" });
            // 生成行
            for (int i = 0; i < 50; i++) {
                dr = dt.NewRow();
                // 获取当前行的列
                for (int j = 0; j < dt.Columns.Count; j++) {
                    dr[j] = null;
                    //if (i < aaa.Count) {
                    //    dr[j] = aaa[i][j];
                    //} else {
                    //    dr[j] = null;
                    //}
                }
                dt.Rows.Add(dr);
            }
            view.DataSource = dt;
            // 行标题宽度
            view.RowHeadersWidth = 60;
            // 生成行标题
            for (int i = 0; i < inputDataGridView.RowCount; i++) {
                view.Rows[i].HeaderCell.Value = (i+1).ToString();
            }  
        }
        /// <summary>
        /// 生成最后结果的方法
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        private string beGetSetTotalMethod(List<Dictionary<string, string>> dataDic) {
            // 验证
            if(!checkProperty()) return "";
            // 生成最终结果
            string retStr = beClassMethod(dataDic);
            return retStr;
        }
        // 生成主体类的方法
        private string beClassMethod(List<Dictionary<string, string>> dataDic) { 
            StringBuilder retStrBui = new StringBuilder();
            if(dataDic != null && dataDic.Count > 0) {
                // 生成包
                retStrBui.AppendLine(bePackageMethod());
                // 生成导包语句
                retStrBui.AppendLine(beImportPackageMethod());
                // 生成序列化
                if(isSerialization && 生成序列化_check.Checked && 生成序列化_check.Enabled) {
                    string serStr = beSerializationMethod();
                    retStrBui.AppendLine(serStr);
                } else { 
                    retStrBui.AppendLine("public class "+classNameStr+" {");
                }
                // 生成字段
                string fieldStr = beFieldMethod(dataDic);
                retStrBui.AppendLine(fieldStr);
                // 生成构造函数
                if(isConstructor && 生成构造函数_check.Checked && 生成构造函数_check.Enabled) { 
                    string constructorStr = beConstructorMethod(dataDic);
                    if(constructorStr != null && !"".Equals(constructorStr)) { 
                        retStrBui.AppendLine(LINE+constructorStr);
                    }
                }
                // 生成 get set
                if(isGetAndSet && 生成get_set_check.Checked && 生成get_set_check.Enabled) { 
                    // 生成构造函数
                    string getsetStr = beGetSetMethod(dataDic);
                    if(getsetStr != null && !"".Equals(getsetStr)) { 
                        retStrBui.AppendLine(LINE+getsetStr);
                    }
                }
                // 生成深克隆
                if(isDeepClone && 生成深拷贝方法_check.Checked && 生成深拷贝方法_check.Enabled) { 
                    string deepCloneStr = beDeepCloningMethod(dataDic);
                    if(deepCloneStr != null && !"".Equals(deepCloneStr)) { 
                        retStrBui.AppendLine(LINE+deepCloneStr);
                    }
                }
                // 生成toString
                if(isToString && 生成ToString_check.Checked && 生成ToString_check.Enabled) { 
                    string toStringStr = beToStringMethod(dataDic);
                    if(toStringStr != null && !"".Equals(toStringStr)) { 
                        retStrBui.AppendLine(LINE+toStringStr);
                    }
                }
                retStrBui.Append("}");
            }
            string retStr = retStrBui.ToString();
            return retStr;
        }
        // 生成包的方法
        private string bePackageMethod() { 
            StringBuilder builder = new StringBuilder();
            string retStr = "";
            if (packageNameStr.IndexOf(";").Equals(packageNameStr.Length-1)) { 
                builder.AppendLine("package "+packageNameStr);
            } else { 
                builder.AppendLine("package "+packageNameStr+";");
            }
            // 去除最后的换行符
            retStr = StringUtilsMet.trimEndNewLine(builder.ToString());
            return retStr;
        }
        // 生成导包语句的方法
        private string beImportPackageMethod() { 
            StringBuilder builder = new StringBuilder();
            string retStr = "";
            builder.AppendLine("");
            if(isSerialization && 生成序列化_check.Checked && 生成序列化_check.Enabled) { 
                builder.AppendLine("import java.io.Serializable;"+Environment.NewLine);
            }
            
            retStr = StringUtilsMet.trimEndNewLine(builder.ToString());
            return retStr;
        }
        // 生成序列化的方法
        private string beSerializationMethod() { 
            StringBuilder builder = new StringBuilder();
            string retStr = "";
            // builder.AppendLine("import java.io.Serializable;"+Environment.NewLine);
            builder.AppendLine("public class "+classNameStr+" implements Serializable {");
            builder.AppendLine(TAB_STR+"/** 序列化唯一的ID */");
            builder.Append(TAB_STR+"private static final long serialVersionUID = -"
                +DateTime.Now.Ticks.ToString()+"L;");
            // 去除最后一个换行符
            retStr = builder.ToString();
            return retStr;
        }
        // 生成字段的方法
        private string beFieldMethod(List<Dictionary<string, string>> dataDic) { 
            StringBuilder builder = new StringBuilder();
            string retStr = "";
            if(dataDic != null && dataDic.Count > 0) {
                Dictionary<string, string> dbTypeToJava = getDbTypeToJavaType();
                string modifier = TAB_STR+"private";
                string type = "";
                string name = "";
                string comment = "";
                foreach (Dictionary<string, string> dic in dataDic) {
                    name = StringUtilsMet.charsToHump(getNameTypeComment(dic)[0], 1);
                    if(dbTypeToJava.ContainsKey(dic[columnTypeStr])) type = dbTypeToJava[getNameTypeComment(dic)[1]];
                    comment = getNameTypeComment(dic)[2];
                    // 是否生成字段注释
                    if (isFieldComment && 生成字段注释_check.Enabled) { 
                        builder.AppendLine(TAB_STR+"/** "+ comment +" */");
                    }
                    builder.AppendLine(modifier+" "+type+" "+name+";");
                }
                
            }
            // 去除最后一个换行符
            retStr = StringUtilsMet.trimEndNewLine(builder.ToString());
            return retStr;
        }
        // 生成get set的方法
        private string beGetSetMethod(List<Dictionary<string, string>> dataDic) { 
            StringBuilder builder = new StringBuilder();
            string retStr = "";
            if(dataDic != null && dataDic.Count > 0 && 生成get_set_check.Enabled) { 
                Dictionary<string, string> dbTypeToJava = getDbTypeToJavaType();
                string type = "";
                string name = "";
                string comment = "";
                List<string> getL = new List<string>();
                List<string> setL = new List<string>();

                foreach (Dictionary<string, string> dic in dataDic) {
                    name = getNameTypeComment(dic)[0];
                    type = getNameTypeComment(dic)[1];
                    comment = getNameTypeComment(dic)[2];
                    // 判断是否生成get set
                    if (isGetAndSet && 生成get_set_check.Enabled) {
                        string getStr = "";
                        string setStr = "";
                        if (getAndSetType== 0 || getAndSetType == 1) { 
                            getStr = beOneGetMethod(name, type, comment);
                            getL.Add(getStr);
                        }
                        if (getAndSetType== 0 || getAndSetType == 2) { 
                            setStr = beOneSetMethod(name, type, comment);
                            setL.Add(setStr);
                        }  
                    }
                }
                // 判断生成方式
                if (getAndSetRule == 0) { // 对中
                    int len = getL.Count >= setL.Count? getL.Count : setL.Count;
                    string gettStr = "";
                    string settStr = "";
                    for (int i=0; i< len; i++) { 
                        if(i < getL.Count && getL[i] != null && !"".Equals(getL[i])) { 
                            gettStr = getL[i] + LINE;
                        } 
                        if(i < setL.Count && setL[i] != null && !"".Equals(setL[i])) { 
                            settStr = setL[i] + LINE;
                        } 
                        builder.Append(gettStr + settStr);
                    }
                }else if(getAndSetRule == 1){ 
                    for (int i=0; i< getL.Count; i++) { 
                        if(getL[i] != null && !"".Equals(getL[i])) builder.Append(getL[i] + LINE);
                    }
                    for (int i=0; i< setL.Count; i++) { 
                        if(setL[i] != null && !"".Equals(setL[i])) builder.Append(setL[i] + LINE);
                    }
                }else if(getAndSetRule == 2){ 
                    for (int i=0; i< setL.Count; i++) { 
                        if(setL[i] != null && !"".Equals(setL[i])) builder.Append(setL[i] + LINE);
                    }
                    for (int i=0; i< getL.Count; i++) { 
                        if(getL[i] != null && !"".Equals(getL[i])) builder.Append(getL[i] + LINE);
                    }
                }
                retStr = builder.ToString();
                if(retStr.Length >= LINE.Length) { 
                    retStr = retStr.Substring(0, retStr.Length - LINE.Length);    
                }
            }
            return retStr;
        }
        // 生成一个get方法
        private string beOneGetMethod(string name, string dbtype, string zs) {
            StringBuilder builder = new StringBuilder();
            Dictionary<string, string> dbTypeToJava = getDbTypeToJavaType();
            string type = dbTypeToJava.ContainsKey(dbtype)? dbTypeToJava[dbtype] : "";
            string getname = StringUtilsMet.charsToHump(name, 0);
            string getname2 = StringUtilsMet.charsToHump(name, 1);
            string prefix = "get";
            // 判断生成注释
            if (isGetAndSet && isGetSetComment && 生成get_set_check.Enabled && 生成get_set注释_check.Enabled) {
                builder.AppendLine(TAB_STR+"/** "+ zs +" */");
            }
            // 根据类型判断前缀
            if ("boolean".Equals(type)) prefix = "is";
            builder.AppendLine(TAB_STR+"public "+ type +" "+prefix+ getname+"() {");
            builder.AppendLine(TAB_STR+TAB_STR+"return this."+ getname2+";");
            builder.Append(TAB_STR+"}");
            return builder.ToString();
        }
        // 生成一个set方法
        private string beOneSetMethod(string name, string dbtype, string zs) {
            StringBuilder builder = new StringBuilder();
            Dictionary<string, string> dbTypeToJava = getDbTypeToJavaType();
            string type = dbTypeToJava.ContainsKey(dbtype)? dbTypeToJava[dbtype] : "";
            string setname = StringUtilsMet.charsToHump(name, 0);
            string setname2 = StringUtilsMet.charsToHump(name, 1);
            // 判断生成注释
            if (isGetAndSet && isGetSetComment && 生成get_set_check.Enabled && 生成get_set注释_check.Enabled) {
                builder.AppendLine(TAB_STR+"/** "+ zs +" */");
            }
            builder.AppendLine(TAB_STR+"public void set"+ setname+"("+type+" "+setname2+") {");
            builder.AppendLine(TAB_STR+TAB_STR+"this."+ setname2 + " = " + setname2+";");
            builder.Append(TAB_STR+"}");
            return builder.ToString();
        }
        // 生成构造函数
        private string beConstructorMethod(List<Dictionary<string, string>> dataDic) { 
            StringBuilder builder = new StringBuilder();
            string retStr = "";
            if(dataDic != null && dataDic.Count > 0 && 生成构造函数_check.Enabled) {
                Dictionary<string, string> dbTypeToJava = getDbTypeToJavaType();
                int index = 0;
                string type = "";
                string name = "";
                string comment = "";
                // 方法注释字符串
                StringBuilder commentVarBui = new StringBuilder(TAB_STR+"/**"
                    +LINE+TAB_STR+"* 自动生成的有参构造器，用来在实例化对象时赋值属性"+LINE);
                // 方法参数字符串
                StringBuilder methodVarBui = new StringBuilder();
                // 内部参数
                StringBuilder interiorVarBui = new StringBuilder(TAB_STR+TAB_STR+"super();"+LINE);
                foreach (Dictionary<string, string> dic in dataDic) { 
                    name = StringUtilsMet.charsToHump(getNameTypeComment(dic)[0], 1);
                    type = dbTypeToJava.ContainsKey(getNameTypeComment(dic)[1])? dbTypeToJava[getNameTypeComment(dic)[1]] : "";
                    comment = getNameTypeComment(dic)[2];
                    // 判断生成注释
                    if(isConstructor && isConstrComment && 构造器生成注释_check.Enabled) { 
                        commentVarBui.AppendLine(TAB_STR+"* @param "+name +" - "+comment);
                    } else { if(commentVarBui.Length > 0)commentVarBui.Clear();}
                    // 生成构造函数
                    if(isConstructor && 生成构造函数_check.Enabled) { 
                        index++;
                        // 生成方法参数
                        if(index <= 6) {
                            methodVarBui.Append(type+" "+ name+",");
                        } else { 
                            methodVarBui.Append(LINE+TAB_STR+TAB_STR+type+" "+ name+",");
                            index = 0;
                        }
                        // 生成内部参数
                        interiorVarBui.AppendLine(TAB_STR+TAB_STR+"this."+name +" = "+name+";");
                    }
                }
                string commentVar = commentVarBui.ToString()+TAB_STR+"*/";
                // 去除最后一个逗号
                string methodVar = methodVarBui.ToString();
                if(methodVar.Length >= 1) methodVar = methodVar.Substring(0, methodVar.Length - 1);
                // 去除最后一个换行符
                string interiorVar = StringUtilsMet.trimEndNewLine(interiorVarBui.ToString());
                // 组装最后的字符串
                // 生成无参构造器
                builder.AppendLine(TAB_STR+"/** 无参构造器 */");
                builder.AppendLine(TAB_STR+"public "+classNameStr+"() {}");
                builder.AppendLine(commentVar);
                builder.Append(TAB_STR+"public "+classNameStr+" (");
                builder.AppendLine(methodVar + ") {");
                builder.AppendLine(interiorVar);
                builder.Append(TAB_STR+"}");
            }
            retStr = builder.ToString();
            return retStr;
        }
        // 生成深克隆的方法
        private string beDeepCloningMethod(List<Dictionary<string, string>> dataDic) { 
            StringBuilder builder = new StringBuilder();
            string retStr = "";
            if(dataDic != null && dataDic.Count > 0 && isDeepClone && 生成深拷贝方法_check.Enabled) { 
                Dictionary<string, string> dbTypeToJava = getDbTypeToJavaType();
                string type = "";
                string name = "";
                string setName = "";
                string comment = "";
                string claName = "tempEntity";
                // 内部参数
                StringBuilder interiorVarBui = new StringBuilder(classNameStr+" "+claName+"= new "+classNameStr+"();"+LINE);
                foreach (Dictionary<string, string> dic in dataDic) { 
                    name = StringUtilsMet.charsToHump(getNameTypeComment(dic)[0], 1);
                    setName = "set"+StringUtilsMet.charsToHump(getNameTypeComment(dic)[0], 0);
                    type = dbTypeToJava.ContainsKey(getNameTypeComment(dic)[1])? dbTypeToJava[getNameTypeComment(dic)[1]] : "";
                    comment = getNameTypeComment(dic)[2];
                    interiorVarBui.AppendLine(TAB_STR+TAB_STR+claName+"."+setName+"("+"new "+type+"(this."+name+"));");
                }
                // 组装最后的字符串
                builder.AppendLine(TAB_STR + "/** 自动生成的深克隆方法，将对象中的每项进行深克隆 */");
                builder.AppendLine(TAB_STR+"public "+classNameStr+" DeepCloning() {");
                // 去除最后一个换行符
                string interiorVar = StringUtilsMet.trimEndNewLine(interiorVarBui.ToString());
                builder.AppendLine(TAB_STR+TAB_STR+interiorVar);
                builder.AppendLine(TAB_STR+TAB_STR+"return "+claName+";");
                builder.Append(TAB_STR+"}");
            }
            retStr = builder.ToString();
            return retStr;
        }
        // 生成toString方法
        private string beToStringMethod(List<Dictionary<string, string>> dataDic) { 
            StringBuilder builder = new StringBuilder();
            string retStr = "";
            if(dataDic != null && dataDic.Count > 0 && isToString && 生成ToString_check.Enabled) { 
                Dictionary<string, string> dbTypeToJava = getDbTypeToJavaType();
                int index = 0;
                string type = "";
                string name = "";
                string comment = "";
                // 内部参数
                StringBuilder interiorVarBui = new StringBuilder(TAB_STR+TAB_STR+"return \""+classNameStr+"：[");
                foreach (Dictionary<string, string> dic in dataDic) { 
                    index++;
                    name = StringUtilsMet.charsToHump(getNameTypeComment(dic)[0], 1);
                    type = dbTypeToJava.ContainsKey(getNameTypeComment(dic)[1])? dbTypeToJava[getNameTypeComment(dic)[1]] : "";
                    comment = getNameTypeComment(dic)[2];
                    if (index <= 4) { 
                        interiorVarBui.Append(name+"=\" + "+name+" + \", ");
                    } else { 
                        if(interiorVarBui.Length >= 6){ 
                            interiorVarBui.Remove(interiorVarBui.ToString().Length-6, 6);
                        } 
                        interiorVarBui.Append(LINE+TAB_STR+TAB_STR+TAB_STR+TAB_STR+" + \""+name+"=\" + "+name+" + \", ");
                        index = 0;
                    }
                }
                builder.AppendLine(TAB_STR + "/** 重写后的toString方法 */");
                builder.AppendLine(TAB_STR + "@Override");
                builder.AppendLine(TAB_STR + "public String toString() {");
                string interiorVar = interiorVarBui.ToString();
                if (interiorVar.Length >= 2) interiorVar = interiorVar.Substring(0, interiorVar.Length-2)+"]\"";
                builder.AppendLine(interiorVar+";");
                builder.AppendLine(TAB_STR + "}");
            }
            retStr = builder.ToString();
            return retStr;
        }
        // 判断全选反选复选框
        private void isSelectAllChecked() { 
            List<bool> boos = new List<bool>();
            ControlsUtilsMet.getControlsChecked(ref boos, 结果选项容器.Controls);
            // 获取选中的的复选框的个数
            int trueL = boos.Where(b=> b.Equals(true)).ToArray().Length;
            // 获取未选中的的复选框的个数
            int falseL = boos.Where(b=> b.Equals(false)).ToArray().Length;
            if(falseL == 0) { 
                全选反选_check.ThreeState = false;
                全选反选_check.CheckState = CheckState.Checked;
                全选反选_check.Checked = true;
            }else if (trueL == 0) { 
                全选反选_check.ThreeState = false;
                全选反选_check.CheckState = CheckState.Unchecked;
                全选反选_check.Checked = false;
            } else { 
                全选反选_check.ThreeState = true;
                全选反选_check.CheckState = CheckState.Indeterminate;
            }
        }
        /// <summary>
        /// 将数据表格的内容转化为要生成get set方法所需的List<string[]>
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        private List<Dictionary<string, string>> dateTabelStrToList(DataGridView dataGrid) {
            if(dataGrid == null) return null;
            List<Dictionary<string, string>> retList = null;
            DataGridViewColumnCollection cc = dataGrid.Columns;
            DataGridViewRowCollection rc = dataGrid.Rows;
            if (cc.Count >= 3) {
                Dictionary<string, string> rowDic = null;
                retList = new List<Dictionary<string, string>>();
                foreach (DataGridViewRow forRow in rc) {
                    rowDic = new Dictionary<string, string>();
                    for (int i=0; i < forRow.Cells.Count; i++) {
                        DataGridViewCell cell = forRow.Cells[i];
                        string val = cell != null && cell.Value != null? cell.Value.ToString() : "";
                        string key = i<= cc.Count? cc[i].HeaderCell.Value.ToString() : DateTime.Now.Ticks.ToString();
                        rowDic.Add(key, val);
                    }
                    // 取行不全为空的
                    if(rowDic.Values.Where(str=> str != null && !"".Equals(str)).Count() > 0) { 
                        retList.Add(rowDic);
                    }
                }
            }
            return retList;
        }
        // 获取实际要用到的字段名 字段类型 字段注释 
        private string[] getNameTypeComment(Dictionary<string, string> dic) { 
            string[] strArr = new string[] {"","","" };
            Dictionary<string, string> dbTypeToJava = getDbTypeToJavaType();
            if (dic.ContainsKey(columnNameStr)) { 
                strArr[0] = dic[columnNameStr];
            }
            if(dic.ContainsKey(columnTypeStr)) { 
                strArr[1] = dic[columnTypeStr];
            }
            if (dic.ContainsKey(columnAnnotateStr)) { 
                strArr[2] = dic[columnAnnotateStr];
            }
            return strArr;
        }
        // key为数据库类型 value为对于JAVA类型
        private static Dictionary<string, string> getDbTypeToJavaType() { 
            Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            dic.Add("CHAR","String");
            dic.Add("VARCHAR","String");
            dic.Add("LONGVARCHAR","String");
            dic.Add("NUMERIC","java.math.BigDecimal");
            dic.Add("DECIMAL","java.math.BigDecimal");
            dic.Add("BIT","Boolean");
            dic.Add("TINYINT","Integer");
            dic.Add("SMALLINT","Integer");
            dic.Add("INTEGER","Integer");
            dic.Add("INT","Integer");
            dic.Add("BIGINT","Long");
            dic.Add("REAL","Float");
            dic.Add("FLOAT","Double");
            dic.Add("DOUBLE","Double");
            dic.Add("BINARY","byte[]");
            dic.Add("VARBINARY","byte[]");
            dic.Add("LONGVARBINARY","byte[]");
            dic.Add("DATE","java.sql.Date");
            dic.Add("TIME","java.sql.Time");
            dic.Add("TIMESTAMP","java.sql.Timestamp");
            return dic;
        }
        // 选项卡选择事件
        private void tab容器_Selecting(object sender, TabControlCancelEventArgs e) {
            TabControl tabControl = (TabControl)sender;
            if(e.TabPageIndex == 0 && inputDataGridView == null) { 
                // 加载数据表格配置
                initInputDataViewConf();
                // 数据表格生成数据
                initDataViewStr(inputDataGridView);
            }
        }
        // 下拉框鼠标移入事件
        private void ComboBox_MouseEnter(object sender, EventArgs e) {
            ComboBox combo = (ComboBox)sender;
            //if(combo.Equals(输入_生成到_comB)) { 
            //    combo.DroppedDown = true;
            //}
        }
        // 复选框鼠标点击事件事件
        private void CheckBox_MouseDown(object sender, MouseEventArgs e) {
            if (MouseButtons.Left.Equals(e.Button)) {
                CheckBox checkBox = (CheckBox)sender;
                bool bl = !checkBox.Checked;
                CheckState checkState = checkBox.CheckState;
                //if (全选反选_check.Equals(checkBox)) {
                //    checkBox.ThreeState = false;
                //    ControlsUtilsMet.setControlsChecked(结果选项容器.Controls, bl);
                //} //else if (生成get_set_check.Equals(checkBox)) {
                    //        isGetAndSet = bl;
                    //        get_set规则_check.Enabled = bl;
                    //        生成get_set_red.Enabled = bl;
                    //        只生成get_rad.Enabled = bl;
                    //        只生成set_rad.Enabled = bl;
                    //        get_set对中_red.Enabled = bl;
                    //        先get后set_red.Enabled = bl;
                    //        先set后get_red.Enabled = bl;
                    //        生成get_set注释_check.Enabled = bl && 生成注释_check.Checked;
                    //    }else if (生成构造函数_check.Equals(checkBox)) {
                    //        isConstructor = bl;
                    //        构造器生成注释_check.Enabled = bl && 生成注释_check.Checked;
                    //    }else if (生成注释_check.Equals(checkBox)) { 
                    //        isComment = bl;
                    //        生成字段注释_check.Enabled = bl;
                    //        生成get_set注释_check.Enabled = bl && 生成get_set_check.Checked;
                    //        构造器生成注释_check.Enabled = bl && 生成构造函数_check.Checked;
                    //    }else if (生成深拷贝方法_check.Equals(checkBox)) { 
                    //        isDeepClone = bl;
                    //    }else if (生成ToString_check.Equals(checkBox)) { 
                    //        isToString = bl;
                    //    }else if (生成字段注释_check.Equals(checkBox)) { 
                    //        isFieldComment = bl;
                    //    }else if (生成get_set注释_check.Equals(checkBox)) { 
                    //        isGetSetComment = bl;
                    //    }else if (构造器生成注释_check.Equals(checkBox)) { 
                    //        isConstrComment = bl;
                    //    }else if (生成序列化_check.Equals(checkBox)) { 
                    //        isSerialization = bl;
                    //    }
                }
        }
        // 复选框选项改变事件
        private void CheckBox_CheckedChanged(object sender, EventArgs e) {
            CheckBox checkBox = (CheckBox)sender;
            bool bl = checkBox.Checked;
            CheckState checkState = checkBox.CheckState;
            if (全选反选_check.Equals(checkBox)) {
                if (checkBox.Capture) { 
                    checkBox.ThreeState = false;
                    ControlsUtilsMet.setControlsChecked(结果选项容器.Controls, bl); 
                }
                
            }else if (生成get_set_check.Equals(checkBox)) { 
                isGetAndSet = bl;
                get_set规则_check.Enabled = bl;
                生成get_set_red.Enabled = bl;
                只生成get_rad.Enabled = bl;
                只生成set_rad.Enabled = bl;
                get_set对中_red.Enabled = bl;
                先get后set_red.Enabled = bl;
                先set后get_red.Enabled = bl;
                生成get_set注释_check.Enabled = bl && 生成注释_check.Checked;
            }else if (生成构造函数_check.Equals(checkBox)) {
                isConstructor = bl;
                构造器生成注释_check.Enabled = bl && 生成注释_check.Checked;
            }else if (生成注释_check.Equals(checkBox)) { 
                isComment = bl;
                生成字段注释_check.Enabled = bl;
                生成get_set注释_check.Enabled = bl && 生成get_set_check.Checked;
                构造器生成注释_check.Enabled = bl && 生成构造函数_check.Checked;
            }else if (生成深拷贝方法_check.Equals(checkBox)) { 
                isDeepClone = bl;
            }else if (生成ToString_check.Equals(checkBox)) { 
                isToString = bl;
            }else if (生成字段注释_check.Equals(checkBox)) { 
                isFieldComment = bl;
            }else if (生成get_set注释_check.Equals(checkBox)) { 
                isGetSetComment = bl;
            }else if (构造器生成注释_check.Equals(checkBox)) { 
                isConstrComment = bl;
            }else if (生成序列化_check.Equals(checkBox)) { 
                isSerialization = bl;
            }else if (get_set规则_check.Equals(checkBox)) { 
                checkBox.Checked = true;
            }
            if(!全选反选_check.Equals(checkBox) && !全选反选_check.Capture)  isSelectAllChecked();
        }
        // 单选按钮鼠标点击事件
        private void RadioButton_MouseDown(object sender, MouseEventArgs e) {
            //if (MouseButtons.Left.Equals(e.Button)) { 
            //    RadioButton radio = (RadioButton)sender;
            //    bool bl = !radio.Checked;
            //    if (生成get_set_red.Equals(radio)) { 
            //        getAndSetType = 0;
            //    }else if (只生成get_rad.Equals(radio)) { 
            //        getAndSetType = 1;
            //    }else if (只生成set_rad.Equals(radio)) { 
            //        getAndSetType = 2;
            //    }else if (get_set对中_red.Equals(radio)) { 
            //        getAndSetRule = 0;
            //    }else if (先get后set_red.Equals(radio)) { 
            //        getAndSetRule = 1;
            //    }else if (先set后get_red.Equals(radio)) { 
            //        getAndSetRule = 2;
            //    }
            //}
        }
        // 单选按钮选项改变事件
        private void RadioButton_CheckedChanged(object sender, EventArgs e) { 
            RadioButton radio = (RadioButton)sender;
            bool bl = radio.Checked;
            if (生成get_set_red.Equals(radio)) { 
                getAndSetType = 0;
            }else if (只生成get_rad.Equals(radio)) { 
                getAndSetType = 1;
            }else if (只生成set_rad.Equals(radio)) { 
                getAndSetType = 2;
            }else if (get_set对中_red.Equals(radio)) { 
                getAndSetRule = 0;
            }else if (先get后set_red.Equals(radio)) { 
                getAndSetRule = 1;
            }else if (先set后get_red.Equals(radio)) { 
                getAndSetRule = 2;
            }
        }
        // 下拉框选择子项事件
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            ComboBox comB = (ComboBox)sender;
            if(输入_生成到_comB.Equals(comB) && comB.Focused) {

                List<Dictionary<string, string>> dataDic = dateTabelStrToList(inputDataGridView);
                string str = beGetSetTotalMethod(dataDic);
                if ("源文本框".Equals(comB.SelectedItem)) { 
                    TextBox t = TextBoxCache.UpOperatingTextBox;
                    if (t != null) { 
                        if(str != null && str.Length > 0) t.Text = str;
                    } else { 
                        MessageBox.Show("获取的源文本框为NULL");    
                    }
                }else if ("记事本".Equals(comB.SelectedItem) && comB.Focused){ 
                    if(str != null && str.Length > 0) FileUtilsMet.turnOnNotepad(str);
                }
            }
            
        }
        // 文本框文本改变事件
        private void TextBox_TextChanged(object sender, EventArgs e) {
            TextBox t = (TextBox)sender;
            if(t.Equals(输入_类名_textB)) { 
                classNameStr = t.Text;
            }else if(t.Equals(输入_包名_textB)) { 
                packageNameStr = t.Text;
            }
        }
    }
}
