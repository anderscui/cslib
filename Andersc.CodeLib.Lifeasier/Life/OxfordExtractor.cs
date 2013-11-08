using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Andersc.CodeLib.Common;
using Andersc.CodeLib.Common.Extension;
using Andersc.CodeLib.Lifeasier.Components;

namespace Andersc.CodeLib.Lifeasier.Life
{
    public partial class OxfordExtractor : DialogForm
    {
        private static readonly char Separator = ':';
        private static readonly char NewLine = '\n';

        private static readonly string RegexNewLine = "\\n";
        private static readonly string RegexSep = ":";
        private static readonly string RegexSubSep = "(\\(\\w\\).+?)";
        private static readonly string RegexPartOfSpeech = "(\\[[\\x00-\\x7F].*\\])";
        private static readonly string RegexUsage = "(\\(\\w+\\s+[^\\u0000-\\u0080]+?\\))";

        public OxfordExtractor()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            SetSwitchButtonText();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Extract();
        }

        private void Extract()
        {
            var input = txtInput.Text;
            txtOutput.Text += input.Count(c => c == Separator) + Environment.NewLine;
            txtOutput.Text += input.Count(c => c == NewLine) + Environment.NewLine;

            var meaningList = new List<string>();
            foreach (string meaning in input.Split('\n'))
            {
                var val = meaning;
                var subMeanings = Regex.Matches(val, RegexSubSep);
                if (subMeanings.Count == 0)
                {
                    meaningList.Add(val);
                }
                else
                {
                    for (int i = 0; i < subMeanings.Count - 1; i++)
                    {
                        var curIndex = subMeanings[i].Index;
                        var nextIndex = subMeanings[i + 1].Index;
                        var subMeaning = val.Substring(subMeanings[i].Index, nextIndex - curIndex);
                        meaningList.Add(subMeaning);
                    }

                    meaningList.Add(val.Substring(subMeanings[subMeanings.Count - 1].Index));
                }
            }

            txtOutput.Clear();

            var chineseMeanings = meaningList
                                    .Select(m => m.Split(Separator)[0])
                                    .Select(m => m.Substring(FirstChineseIndex(m)))
                                    .Select(m => m.Replace("; ", "，"))
                                    .Select(m => m.Replace(";", "，"))
                                    .Select(m => m.Replace(", ", "，"))
                                    .Select(m => m.Replace(",", "，"));
            txtOutput.Text = string.Join("；", chineseMeanings);
        }

        private int FirstChineseIndex(string input)
        {
            var matchPartOfSpeech = Regex.Match(input, RegexPartOfSpeech);
            var matchUsage = Regex.Match(input, RegexUsage);

            var skipPartOfSpeech = 0;
            var skipUsage = 0;

            if (matchPartOfSpeech.Success)
            {
                skipPartOfSpeech = matchPartOfSpeech.Index + matchPartOfSpeech.Length;
            }
            if (matchUsage.Success)
            {
                skipUsage = matchUsage.Index + matchUsage.Length;
            }

            var skip = Math.Max(skipPartOfSpeech, skipUsage);

            if (!matchPartOfSpeech.Success)
            {
                var unmatchedBracket = input.IndexOf(']');
                if (unmatchedBracket >= 0 
                    && !input.Substring(0, unmatchedBracket).Contains('['))
                {
                    skip = Math.Max(skip, unmatchedBracket);
                }
            }

            var start = (skip > 0) ? skip : 0;
            for (int i = start; i < input.Length; i++)
            {
                if (input[i].IsChineseChar())
                {
                    if (i > 0 && input[i - 1] == '（')
                    {
                        return i - 1;
                    }
                    return i;
                }
            }

            return 0;
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            if (txtInput.Text.Length > 0)
            {
                Extract();

                Clipboard.SetText(txtOutput.Text);
                lastText = txtOutput.Text;
                //hasChecked = true;
            }
            else
            {
                txtOutput.Clear();
            }
        }

        private void btnShowClipboard_Click(object sender, EventArgs e)
        {
            //var dobj = Clipboard.GetDataObject();
            //MessageBox.Show("t");
        }

        private string lastText = string.Empty;
        //private bool hasChecked = false;
        private void clipboardTimer_Tick(object sender, EventArgs e)
        {
            var text = Clipboard.GetText();
            if (text.IsNotBlank() && text != lastText)
            {
                txtInput.Text = text;
            }
        }

        private void SetSwitchButtonText()
        {
            btnSwitch.Text = clipboardTimer.Enabled
                                 ? "Monitoring Clipboard"
                                 : "Not Monitoring Clipboard";
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            clipboardTimer.Enabled = !clipboardTimer.Enabled;
            SetSwitchButtonText();
        }
    }
}
