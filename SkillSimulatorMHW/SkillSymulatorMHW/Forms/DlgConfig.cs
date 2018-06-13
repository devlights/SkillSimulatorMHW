﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SkillSimulatorMHW.Data;
using SkillSimulatorMHW.Defines;
using SkillSimulatorMHW.Extensions;

namespace SkillSimulatorMHW.Forms
{
    /// <summary>
    /// 動作設定ダイアログクラス.
    /// </summary>
    public partial class DlgConfig : Form
    {
        /// <summary>
        /// コンストラクタ.
        /// </summary>
        public DlgConfig()
        {
            InitializeComponent();

            // コンボボックス生成.
            this.cmbAnalyzeType.Init(new List<CmbItem<AnalyzeType>>
            {
                new CmbItem<AnalyzeType>("使用しない", AnalyzeType.Non),
                new CmbItem<AnalyzeType>("条件を満たすセットが存在しない場合解析する", AnalyzeType.NotExist),
                new CmbItem<AnalyzeType>("常に解析する", AnalyzeType.Always),
            });
        }

        /// <summary>
        /// コンフィグを反映.
        /// </summary>
        /// <param name="config"></param>
        public void SetConfig(Config config)
        {
            this.spinShowResultLimitCount.Text = config.ShowResultLimitCount.ToString();

            this.spinSerchLimitCount.Text    = config.SerchLimitCount.ToString();
            this.chkUseArmorAbstract.Checked = config.UseArmorAbstract;
            this.chkEnableAsyncExec.Checked  = config.EnableAsyncExec;
            this.cmbAnalyzeType.SelectCmbItem(config.AnalyzeType);

            this.chkShowDebugLog.Checked = config.ShowDebugLog;
        }

        /// <summary>
        /// コンフィグを取得.
        /// </summary>
        /// <returns></returns>
        public Config GetConfig()
        {
            // この画面に表示されていない値を保持するために新たにロード.
            var config = Config.Load();

            // 画面の内容を反映.
            config.ShowResultLimitCount = Int32.Parse(this.spinShowResultLimitCount.Text);

            config.SerchLimitCount = Int32.Parse(this.spinSerchLimitCount.Text);
            config.UseArmorAbstract = this.chkUseArmorAbstract.Checked;
            config.EnableAsyncExec = this.chkEnableAsyncExec.Checked;

            config.AnalyzeType = this.cmbAnalyzeType.SelectedCmbItem<AnalyzeType>();

            config.ShowDebugLog = this.chkShowDebugLog.Checked;

            return config;
        }

        /// <summary>
        /// ダイアログロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CallBackDlgConfigLoad(object sender, System.EventArgs e)
        {
            // 現在の設定を画面に反映.
            this.SetConfig(Ssm.Config);
        }

        /// <summary>
        /// デフォルトに戻すボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CallBackBtnDefaultClick(object sender, EventArgs e)
        {
            // デフォルトの設定を画面に反映.
            this.SetConfig(new Config());
        }
    }
}