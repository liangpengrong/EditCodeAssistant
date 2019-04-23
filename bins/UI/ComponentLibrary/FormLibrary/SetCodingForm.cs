using Core.CacheLibrary.ControlCache;
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
    /// 设置编码窗体
    /// </summary>
    public partial class SetCodingForm : Form, IComponentInitMode<Form> {
        // 要操作的文本框
        private TextBox textBox;
        // 文本框的编码
        private Encoding textCoding = null;

        internal SetCodingForm() {
            InitializeComponent();
        }
        /// <summary>
        /// 打开单例模式下的查找和替换窗口
        /// </summary>
        /// <returns></returns>
        public Form initSingleExample(bool isShowTop){
            SetCodingForm formThis = null;
            Form form = FormCacheFactory.getSingletonCache(DefaultNameEnum.SET_CODING_FORM);
            if(form == null || form.IsDisposed || !(form is SetCodingForm)) { 
                formThis = this;
                formThis.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.SET_CODING_FORM);
                // 将窗体放入单例窗体工厂中
                formThis = FormCacheFactory.ininSingletonForm(formThis, false);
            } else {
                formThis = (SetCodingForm) form;
                formThis.Activate();
            }
            if(isShowTop) FormCacheFactory.addTopFormCache(formThis);
            formThis.MinimumSize = formThis.Size;
            formThis.Visible = false;
            return formThis;
        }
        /// <summary>
        /// 打开多例模式下的查找和替换窗口
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Form initPrototypeExample(bool isShowTop) {
            SetCodingForm formThis = this;
            formThis.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.SET_CODING_FORM)+DateTime.Now.Ticks.ToString();;
            // 加入到顶层窗体集合
            if(isShowTop) FormCacheFactory.addTopFormCache(formThis);
            // 加入到多例工厂
            FormCacheFactory.addPrototypeCache(DefaultNameEnum.SET_CODING_FORM, formThis);
            formThis.Activate();
            formThis.Visible = false;
            return formThis;
        }
        // 窗体加载事件
        private void SetCodingForm_Load(object sender, EventArgs e) {
            initData();
            // 将文本框的编码赋值到label中
            textCodinCopyLab();
            setCodingSet();
        }
        // 初始化数据
        private void initData() { 
            // 初始化源数据控件
            initSourceDataCon();
        }
        /// <summary>
        /// 初始化源数据控件 这里是文本框
        /// </summary>
        private void initSourceDataCon() {
            if (textBox == null) { 
                Control conTab = ControlCacheFactory.getSingletonCache(DefaultNameEnum.TAB_CONTENT);
                List<TextBox> controls = null;
                if (conTab != null && conTab is TabControl) { 
                    ControlsUtilsMet.GetAllControlByType(ref controls, ((TabControl)conTab).SelectedTab.Controls);
                    if (controls.Count > 0 && controls[0] is TextBox) { 
                        textBox = controls[0];
                    }
                }
            }
        }
        /// <summary>
        /// 将文本框的编码赋值到label中
        /// </summary>
        private void textCodinCopyLab() {
            Dictionary<string, object> textDic = TextBoxUtilsMet.GetTextTagToMap(textBox);
            if(textDic.ContainsKey(TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING)){ 
                Encoding coding = (Encoding)textDic[TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING];
                textCoding = coding;
                get_coding.Text = coding.BodyName.ToUpper();    
            } else { 
                get_coding.Text = Encoding.UTF8.BodyName.ToUpper();
                textCoding = Encoding.UTF8;
            }
        }
        /// <summary>
        /// 设置下拉列表框的值
        /// </summary>
        private void setCodingSet() {
            Dictionary<int, string> encDic = FileUtilsMet.GetBrieflyFileEncodingInfo();
            Dictionary<object, string> encDic2 = new Dictionary<object, string>();
            // 将int转为object
            foreach(KeyValuePair<int,string> kvp in encDic) { 
                encDic2.Add(kvp.Key, kvp.Value);
            }
            // 设置下拉列表框的值
            ControlsUtilsMet.SetComboBoxItems(coding_set, encDic2);
            // 选定与文本框编写相同的项
            coding_set.SelectedValue = textCoding.CodePage;
            // 设置自动匹配
            this.coding_set.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.coding_set.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        /// <summary>
        /// 将文本框编码设置为指定编码格式
        /// </summary>
        private void setTextByEncoding() {
            if(textBox == null) MessageBox.Show("要操作的文本框为NULL"); 
            // 获取起始选中位置和选中长度
            int index = textBox.SelectionStart;
            int selLen = textBox.SelectionLength;
            // 文本框的Tag数据
            Dictionary<string, object> tag = TextBoxUtilsMet.GetTextTagToMap(textBox);
            // 获取选中的项的编码页码
            int codingInt = Encoding.UTF8.CodePage;
            if(coding_set.SelectedValue != null) {
                try {
                    codingInt = int.Parse(coding_set.SelectedValue.ToString());
                } catch (Exception ee) { 
                    MessageBox.Show("选中内容无法转化为编码，将使用默认编码"+ee);
                }
            }

            // 获取选择项的编码
            Encoding coding = Encoding.GetEncoding(codingInt);

            // 获取文本框的文本
            string text = "";
            if(tag.ContainsKey(TextBoxTagKey.SAVE_FILE_PATH)) {
                text = FileUtilsMet.FileRead.Read(tag[TextBoxTagKey.SAVE_FILE_PATH].ToString(), coding);
            } else { 
                text = textBox.Text;
            }
            // 将文本框的文本设置为指定编码格式
            byte[] textBoxBytes = textCoding.GetBytes(text);
            byte[] asciiBytes = Encoding.Convert(textCoding, coding, textBoxBytes);
            textBox.Text = coding.GetString(asciiBytes);

            // 恢复文本框的起始位置和选中长度
            textBox.SelectionStart = index;
            textBox.SelectionLength = selLen;
            // 设置保持在Tag数据中的文本框编码
            TextBoxUtilsMet.TextBoxAddTag(textBox, TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING, coding);
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool isCheck() {
            if(coding_set.Text.Length == 0) { 
                MessageBox.Show("不能输入空");
                return false;
            }
            if(coding_set.SelectedItem == null) { 
                MessageBox.Show("只能输入下拉框范围的值");
                return false;
            }
            return true;
        }
        // 确定按钮的点击事件
        private void button1_Click(object sender, EventArgs e) {
            // 初始化源数据控件
            initSourceDataCon();
            // 验证
            if(!isCheck()) return;
            // 设置编码
            setTextByEncoding();
            this.Close();
        }
    }
}
