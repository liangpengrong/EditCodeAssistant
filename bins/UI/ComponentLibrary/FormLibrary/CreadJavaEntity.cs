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
using UI.ComponentLibrary.MethodLibrary.Interface;
using UI.ComponentLibrary.MethodLibrary.Util;

namespace UI.ComponentLibrary.FormLibrary {
    public partial class CreadJavaEntity : Form,IComponentInitMode<Form> {
        // 输出类型下拉框
        private ComboBox outputComBox = null;
        // 换行符
        private static string LINE = Environment.NewLine;
        // 一个Tab字符
        private static string TAB_STR = "    ";
        // 最终要根据此数组生成get/set 0位-字段类型 1位-字段名称 2位-注释
        private List<string[]> dataStrs = new List<string[]>();
        // 属于手动输入的数据表格
        private DataGridView inputDGV = null;
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
        // 生成构造器注释
        private bool isConstrComment = true;
        // get/set生成类型 0-全部生成 1-只生成get 2-只生成set
        private int getAndSetType = 0;
        // get/set生成类型 0-get/set对中 1-先get后set 2-先set后get
        private int getAndSetRule = 0;
        // 生成深拷贝方法
        private bool isDeepClone = false;
        // 生成ToString方法
        private bool isToString = true;
        // 是否序列化
        private bool isSerialization = false;
        // 类型匹配规则 0-匹配数据库 1-匹配java对象封装类
        private int TypeRule = 0;
        // 默认文本编码
        private Encoding encoding = Encoding.UTF8;
        private string columnNameStr = "字段名称";
        private string columnTypeStr = "字段类型";
        private string columnAnnotateStr = "字段注释";
        internal CreadJavaEntity() {
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
        /// 实例化字段转实体类窗体的单例窗体
        /// </summary>
        /// <param name="isShowTop"></param>
        /// <returns></returns>
        public Form initSingleExample(bool isShowTop) { 
            CreadJavaEntity fieldToJavaEntity = null;
            Form form = FormCacheFactory.getSingletonCache(DefaultNameEnum.CREAD_JAVA_ENTITY);
            if(form == null || form.IsDisposed || !(form is SplitCharsForm)) { 
                fieldToJavaEntity = this;
                fieldToJavaEntity.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.CREAD_JAVA_ENTITY);
                fieldToJavaEntity = FormCacheFactory.ininSingletonForm(fieldToJavaEntity, false);
            } else {
                fieldToJavaEntity = (CreadJavaEntity)form;
                fieldToJavaEntity.Activate();
            }
            if(isShowTop) FormCacheFactory.addTopFormCache(fieldToJavaEntity);
            fieldToJavaEntity.Visible = false;
            return fieldToJavaEntity;

        }
        /// <summary>
        /// 实例化字段转实体类窗体的多例窗体
        /// </summary>
        /// <param name="isShowTop"></param>
        /// <returns></returns>
        public Form initPrototypeExample(bool isShowTop) { 
            CreadJavaEntity fieldToJavaEntity = this;
            fieldToJavaEntity.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.CREAD_JAVA_ENTITY)+DateTime.Now.Ticks.ToString();;
            // 加入到顶层窗体集合
            if(isShowTop) FormCacheFactory.addTopFormCache(fieldToJavaEntity);
            // 加入到多例工厂
            FormCacheFactory.addPrototypeCache(DefaultNameEnum.CREAD_JAVA_ENTITY, fieldToJavaEntity);
            fieldToJavaEntity.Visible = false;
            return this;
        }

        // 窗体加载事件
        private void CreadJavaEntity_Load(object sender, EventArgs e) {
            // 加载窗体默认配置
            initFormDefConfig();
            // 加载数据表格配置
            initInputDataViewConf();
            // 数据表格生成数据
            initDataViewStr(inputDGV);
            // 设置输出类型下拉框
            setOutputComBox();
            // 生成消息提示
            initToolTip();
            // 判断全选复选框
            isSelectAllChecked();
            // 设置编码下拉框
            SetEcodingVal();

            this.输入_类型规则_comB.SelectedIndex = 0;
        }
        // 窗口的默认配置
        private void initFormDefConfig() { 
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.AutoScaleMode = AutoScaleMode.None;
            this.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.CREAD_JAVA_ENTITY);
            // 调节窗口位置
            this.Location = FormUtislMet.MiddleForm(this);
        }
        // 设置输出类型下拉框
        private void setOutputComBox() { 
            // 实例化导出下拉框
            outputComBox = new ExportComBox(new ExportComBoxValEnum[]{ExportComBoxValEnum.EXPORT_EXCEL_VAL });
            // 绑定点击事件
            outputComBox.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            // 加入到容器中
            outputComBox.Location = new Point(输入_生成到_lab.Right+5
                , 输入_生成到_lab.Location.Y - 4);
            输入_生成到_lab.Parent.Controls.Add(outputComBox);
            outputComBox.BringToFront();
        }
        /// <summary>
        /// 初始化手动输入的数据表格配置
        /// </summary>
        private void initInputDataViewConf() {
            RedrawDataTable dataView = new RedrawDataTable();
            
            dataView.ColumnSortMode = false;
            dataView.IsShowLineNumber = true;
            dataView.CellDefaultHeight = 24;
            dataView.ColumnHeadDefaultHeight = 24;
            dataView.Location = new Point(操作区_容器.Location.X, 操作区_容器.Bottom + 5);
            dataView.AllowUserToAddRows = false;
            dataView.AllowUserToResizeRows = false;
            dataView.AllowUserToResizeColumns = false;
            dataView.BringToFront();
            dataView.Width = 操作区_容器.ClientSize.Width;
            dataView.Height = 选项区容器.ClientSize.Height - (dataView.Location.Y-选项区容器.Location.X);
            dataView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataView.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            inputDGV = dataView;
            // 记录到状态栏中
            Control c = UIComponentFactory.getSingleControl(DefaultNameEnum.TOOL_START);
            if(c != null && c is RedrawStatusBar) { 
                RedrawStatusBar bar = (RedrawStatusBar)c;
                bar.SetSourceControl(dataView);
            }
            this.Controls.Add(dataView);
        }

        /// <summary>
        /// 初始化消息提示
        /// </summary>
        private void initToolTip() { 
            Dictionary<Control, string> dic = new Dictionary<Control, string>();
            dic.Add(生成深拷贝方法_check, "只对类型为基础类型的包装类有效");
            foreach (KeyValuePair<Control, string> kvp in dic) { 
                RedrawPromptMessBut but1 = new RedrawPromptMessBut();
                but1.ButtonMess = kvp.Value;
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
            retStr = StringUtilsMet.TrimEndNewLine(builder.ToString());
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
            
            retStr = StringUtilsMet.TrimEndNewLine(builder.ToString());
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
                string modifier = TAB_STR+"private";
                string type = "";
                string name = "";
                string comment = "";
                foreach (Dictionary<string, string> dic in dataDic) {
                    name = getRealName(dic, 0);
                    type = getRealType(dic);
                    comment = getRealComment(dic);
                    // 是否生成字段注释
                    if (isFieldComment && 生成字段注释_check.Enabled) { 
                        builder.AppendLine(TAB_STR+"/** "+ comment +" */");
                    }
                    builder.AppendLine(modifier+" "+type+" "+name+";");
                }
                
            }
            // 去除最后一个换行符
            retStr = StringUtilsMet.TrimEndNewLine(builder.ToString());
            return retStr;
        }
        // 生成get set的方法
        private string beGetSetMethod(List<Dictionary<string, string>> dataDic) { 
            StringBuilder builder = new StringBuilder();
            string retStr = "";
            if(dataDic != null && dataDic.Count > 0 && 生成get_set_check.Enabled) { 
                string type = "";
                string name = "";
                string comment = "";
                List<string> getL = new List<string>();
                List<string> setL = new List<string>();

                foreach (Dictionary<string, string> dic in dataDic) {
                    name = getRealName(dic, 0);
                    type = getRealType(dic);
                    comment = getRealComment(dic);
                    // 判断是否生成get set
                    if (isGetAndSet && 生成get_set_check.Enabled) {
                        string getStr = "";
                        string setStr = "";
                        if (getAndSetType== 0 || getAndSetType == 1) { 
                            getStr = beOneGetMethod(dic);
                            getL.Add(getStr);
                        }
                        if (getAndSetType== 0 || getAndSetType == 2) { 
                            setStr = beOneSetMethod(dic);
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
        private string beOneGetMethod(Dictionary<string, string> dic) {
            StringBuilder builder = new StringBuilder();
            string type = getRealType(dic);
            string fieldName = getRealName(dic, 0);
            string getName = getRealName(dic, 1);
            string comment = getRealComment(dic);
            // 判断生成注释
            if (isGetAndSet && isGetSetComment && 生成get_set_check.Enabled && 生成get_set注释_check.Enabled) {
                builder.AppendLine(TAB_STR+"/** "+ comment +" */");
            }
            // 根据类型判断前缀
            if ("boolean".Equals(type)) getName = "is"+getName.Substring(3, getName.Length-3);
            builder.AppendLine(TAB_STR+"public "+ type +" "+getName+"() {");
            builder.AppendLine(TAB_STR+TAB_STR+"return this."+ fieldName +";");
            builder.Append(TAB_STR+"}");
            return builder.ToString();
        }
        // 生成一个set方法
        private string beOneSetMethod(Dictionary<string, string> dic) {
            StringBuilder builder = new StringBuilder();
            string type = getRealType(dic);
            string fieldName = getRealName(dic, 0);
            string setName = getRealName(dic, 2);
            string comment = getRealComment(dic);
            // 判断生成注释
            if (isGetAndSet && isGetSetComment && 生成get_set_check.Enabled && 生成get_set注释_check.Enabled) {
                builder.AppendLine(TAB_STR+"/** "+ comment +" */");
            }
            builder.AppendLine(TAB_STR+"public void "+setName +"("+type+" "+fieldName+") {");
            builder.AppendLine(TAB_STR+TAB_STR+"this."+ fieldName + " = " + fieldName+";");
            builder.Append(TAB_STR+"}");
            return builder.ToString();
        }
        // 生成构造函数
        private string beConstructorMethod(List<Dictionary<string, string>> dataDic) { 
            StringBuilder builder = new StringBuilder();
            string retStr = "";
            if(dataDic != null && dataDic.Count > 0 && 生成构造函数_check.Enabled) {
                int index = 0;
                string type = "";
                string name = "";
                string comment = "";
                // 方法注释字符串
                StringBuilder commentVarBui = new StringBuilder(TAB_STR+"/**"+LINE);
                // 方法参数字符串
                StringBuilder methodVarBui = new StringBuilder();
                // 内部参数
                StringBuilder interiorVarBui = new StringBuilder(TAB_STR+TAB_STR+"super();"+LINE);
                foreach (Dictionary<string, string> dic in dataDic) { 
                    type = getRealType(dic);
                    name = getRealName(dic, 0);
                    comment = getRealComment(dic);
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
                string interiorVar = StringUtilsMet.TrimEndNewLine(interiorVarBui.ToString());
                // 组装最后的字符串
                // 生成无参构造器
                builder.AppendLine(TAB_STR+"public "+classNameStr+"() {super();}");
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
                string type = "";
                string name = "";
                string setName = "";
                string comment = "";
                string claName = "tempEntity";
                // 内部参数
                StringBuilder interiorVarBui = new StringBuilder(classNameStr+" "+claName+"= new "+classNameStr+"();"+LINE);
                foreach (Dictionary<string, string> dic in dataDic) { 
                    setName = getRealName(dic, 2);
                    name = getRealName(dic, 0);
                    type = getRealType(dic);
                    comment = getRealComment(dic);
                    if (type.IndexOf("BigDecimal") >= 0) { 
                        string setStr = "this."+name+" != null? new "+type+"(this."+name+".toString()) : null";
                        interiorVarBui.AppendLine(TAB_STR+TAB_STR+claName+"."+setName+"("+setStr+");");
                    }else if (type.IndexOf("List") >= 0) { 
                        string setStr = "this."+name+" != null? new Array"+type+"("+name+")  : null";
                        interiorVarBui.AppendLine(TAB_STR+TAB_STR+claName+"."+setName+"("+setStr+");");
                    }else if (type.IndexOf("Map") >= 0) {
                        string setStr = "this."+name+" != null? new Hash"+type+"("+name+")  : null";
                        interiorVarBui.AppendLine(TAB_STR+TAB_STR+claName+"."+setName+"("+setStr+");");
                    } else { 
                        interiorVarBui.AppendLine(TAB_STR+TAB_STR+claName+"."+setName+"("+"this."+name+" != null? new "+type+"(this."+name+") : null);");
                    }
                }
                // 组装最后的字符串
                builder.AppendLine(TAB_STR + "/** 自动生成的深克隆方法，将对象中的每项属性进行深克隆 */");
                builder.AppendLine(TAB_STR+"public "+classNameStr+" deepCloning() {");
                // 去除最后一个换行符
                string interiorVar = StringUtilsMet.TrimEndNewLine(interiorVarBui.ToString());
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
                int index = -1;
                string type = "";
                string name = "";
                string comment = "";
                // 内部参数
                StringBuilder interiorVarBui = new StringBuilder(TAB_STR+TAB_STR+"return \""+classNameStr+"：[");
                foreach (Dictionary<string, string> dic in dataDic) { 
                    index++;
                    name = getRealName(dic, 0);
                    type = getRealType(dic);
                    comment = getRealComment(dic);
                    // 当一行字段为4个时换行显示
                    if (index >0 && index % 4 == 0) { 
                        // 加换行
                        interiorVarBui.Append(LINE);
                        // 加缩进
                        interiorVarBui.Append(TAB_STR+TAB_STR+TAB_STR);
                    }
                    if(index == 0) {
                        interiorVarBui.Append(name+"=\" +"+name);
                    } else { 
                        interiorVarBui.Append("+ \", "+name+"=\" +"+name);
                    }   
                }
                builder.AppendLine(TAB_STR + "@Override");
                builder.AppendLine(TAB_STR + "public String toString() {");
                builder.AppendLine(interiorVarBui.Append("+\"]\";").ToString());
                builder.AppendLine(TAB_STR + "}");
            }
            retStr = builder.ToString();
            return retStr;
        }
        // 判断全选反选复选框
        private void isSelectAllChecked() { 
            List<bool> boos = new List<bool>();
            ControlsUtilsMet.GetControlsChecked(ref boos, 结果选项容器.Controls);
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
        /// 将数据表格的内容转化为要生成get set方法所需的List<Dictionary<string, string>>
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
                    // 取每行的单元格对应的数据
                    for (int i=0; i < forRow.Cells.Count; i++) {
                        DataGridViewCell cell = forRow.Cells[i];
                        // 取该行该列的列头值为key
                        string key = i<= cc.Count? cc[i].HeaderCell.Value.ToString() : DateTime.Now.Ticks.ToString();
                        string val = cell != null && cell.Value != null? cell.Value.ToString() : "";
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
        /// <summary>
        /// 获取真实的name值
        /// </summary>
        /// <param name="name">原始的name</param>
        /// <param name="type">0-字段 1-get方法 2-set方法 3-大驼峰 4-小驼峰</param>
        /// <returns></returns>
        private string getRealName(string name, int type) {
            string str = name;
            switch(type) { 
            case 0:
                str = StringUtilsMet.CharsToHumpChars(name, 1);
                break;
            case 1:
                str = "get"+StringUtilsMet.CharsToHumpChars(name, 0);
                break;
            case 2:
                str = "set"+StringUtilsMet.CharsToHumpChars(name, 0);
                break;
            case 3:
                str = StringUtilsMet.CharsToHumpChars(name, 0);
                break;
            case 4:
                str = StringUtilsMet.CharsToHumpChars(name, 1);
                break;
            }
            return str;
        }
        private string getRealName(Dictionary<string, string> dic, int type) { 
            return getRealName(dic.ContainsKey(columnNameStr)? dic[columnNameStr] : "", type);
        }
        /// <summary>
        /// 获取真实的typr值
        /// </summary>
        /// <param name="type">原始的type</param>
        /// <returns></returns>
        private string getRealType(string type) {
            string str = type;
            Dictionary<string, string> dbTypeToJava = null;
            if (TypeRule == 0) { 
                dbTypeToJava = getDbTypeToJavaType();// 匹配数据库
            }else if (TypeRule == 1) { 
                dbTypeToJava = getTypeToJavaType();// 匹配JAVA
            } else { 
                dbTypeToJava = new Dictionary<string, string>();
            }
            str = dbTypeToJava.ContainsKey(type)? dbTypeToJava[type] : type;
            return str;
        }
        private string getRealType(Dictionary<string, string> dic) { 
            return getRealType(dic.ContainsKey(columnTypeStr)? dic[columnTypeStr] : "");
        }
        /// <summary>
        /// 获取真实的Comment值
        /// </summary>
        /// <param name="type">原始的Comment</param>
        /// <returns></returns>
        private string getRealComment(string comment) {
            string str = comment;
            return str;
        }
        private string getRealComment(Dictionary<string, string> dic) { 
            return getRealComment(dic.ContainsKey(columnAnnotateStr)? dic[columnAnnotateStr] : "");
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
        // key为数据库类型 value为对于JAVA类型
        private static Dictionary<string, string> getTypeToJavaType() { 
            Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            dic.Add("String","String");
            dic.Add("int","Integer");
            dic.Add("boolean","Boolean");
            dic.Add("long","Long");
            dic.Add("float","Float");
            dic.Add("double","Double");
            dic.Add("Object","Object");
            dic.Add("char","Character");
            dic.Add("BigDecimal","java.math.BigDecimal");
            dic.Add("DATE","java.sql.Date");
            dic.Add("TIME","java.sql.Time");
            dic.Add("TIMESTAMP","java.sql.Timestamp");
            dic.Add("Type","java.lang.reflect.Type");
            return dic;
        }
        // 选项卡选择事件
        private void tab容器_Selecting(object sender, TabControlCancelEventArgs e) {
            TabControl tabControl = (TabControl)sender;
            if(e.TabPageIndex == 0 && inputDGV == null) { 
                // 加载数据表格配置
                initInputDataViewConf();
                // 数据表格生成数据
                initDataViewStr(inputDGV);
            }
        }
        // 下拉框鼠标移入事件
        private void ComboBox_MouseEnter(object sender, EventArgs e) {
            ComboBox combo = (ComboBox)sender;
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
                    ControlsUtilsMet.SetControlsChecked(结果选项容器.Controls, bl); 
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
            if(outputComBox != null && outputComBox.Equals(comB) && comB.Focused) {
                List<Dictionary<string, string>> dataDic = dateTabelStrToList(inputDGV);
                // 获取最终内容
                string str = beGetSetTotalMethod(dataDic);
                ExportComBoxValEnum val = ExportComBox.stringToEnum(comB.SelectedValue.ToString());
                switch(val) {
                    case ExportComBoxValEnum.EXPORT_NEW_PAGE_VAL: // 新标签
                        if(str.Length > 0) MainTabControlUtils.ExportNewPage(str);
                    break;
                    case ExportComBoxValEnum.EXPORT_THIS_PAGE_VAL: // 当前标签
                        if(str.Length > 0) ControlsUtilsMet.ExportThisPage(str);
                    break;
                    case ExportComBoxValEnum.EXPORT_JAVA_VAL: // java文件
                        if(str.Length > 0) FileUtilsMet.SaveJavaFile(str, classNameStr, encoding);
                    break;
                    case ExportComBoxValEnum.EXPORT_NOTEBOOK_VAL: // 记事本
                        if(str.Length > 0) FileUtilsMet.TurnOnNotepad(str);
                    break;
                }
            }
            if(输入_类型规则_comB.Equals(comB) && comB.Focused) {
                if ("数据库".Equals(comB.SelectedItem)) { 
                    TypeRule = 0;
                }else if("JAVA对象".Equals(comB.SelectedItem) && comB.Focused){ 
                    TypeRule = 1;
                }
            }
        }
        /// <summary>
        /// 设置编码下拉列表框的值
        /// </summary>
        private void SetEcodingVal() {
            Dictionary<int, string> encDic = FileUtilsMet.GetBrieflyFileEncodingInfo();
            Dictionary<object, string> encDic2 = new Dictionary<object, string>();
            // 将int转为object
            foreach(KeyValuePair<int,string> kvp in encDic) { 
                encDic2.Add(kvp.Key, kvp.Value);
            }
            // 设置下拉列表框的值
            ControlsUtilsMet.SetComboBoxItems(输入_编码_comB, encDic2);
            // 选定与文本框编写相同的项
            输入_编码_comB.SelectedValue = Encoding.UTF8.CodePage;
            // 设置自动匹配
            输入_编码_comB.AutoCompleteMode = AutoCompleteMode.Suggest;
            输入_编码_comB.AutoCompleteSource = AutoCompleteSource.ListItems;
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
        // 编码选项改变事件
        private void 输入_编码_comB_SelectedIndexChanged(object sender, EventArgs e) {
            ComboBox combo = (ComboBox)sender;
            object obj = combo.SelectedValue;
            // 获取选定的编码
            if (obj != null) {
                try { 
                    int code = int.Parse(obj.ToString());
                    encoding = Encoding.GetEncoding(code);
                } catch (Exception ee) { 
                    MessageBox.Show("无法将选中内容转化为编码"+ee);
                }   
            }
        }
    }
}
