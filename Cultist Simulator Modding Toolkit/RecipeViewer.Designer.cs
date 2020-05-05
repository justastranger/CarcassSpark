namespace Cultist_Simulator_Modding_Toolkit
{
    partial class RecipeViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.actionIdTextBox = new System.Windows.Forms.TextBox();
            this.endingTextBox = new System.Windows.Forms.TextBox();
            this.burnimageTextBox = new System.Windows.Forms.TextBox();
            this.startdescriptionTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.craftableCheckBox = new System.Windows.Forms.CheckBox();
            this.hintonlyCheckBox = new System.Windows.Forms.CheckBox();
            this.internaldeckLabel = new System.Windows.Forms.Label();
            this.showInternalDeckButton = new System.Windows.Forms.Button();
            this.requirementsLabel = new System.Windows.Forms.Label();
            this.requirementsDataGridView = new System.Windows.Forms.DataGridView();
            this.elementId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extantreqsDataGridView = new System.Windows.Forms.DataGridView();
            this.extantElementId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extantAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extantrequirementsLabel = new System.Windows.Forms.Label();
            this.tablereqsDataGridView = new System.Windows.Forms.DataGridView();
            this.tableElementId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tablerequirementsLabel = new System.Windows.Forms.Label();
            this.slotLabel = new System.Windows.Forms.Label();
            this.showSlotButton = new System.Windows.Forms.Button();
            this.effectsDataGridView = new System.Windows.Forms.DataGridView();
            this.effectsElementId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.effectsAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.effectsLabel = new System.Windows.Forms.Label();
            this.aspectsDataGridView = new System.Windows.Forms.DataGridView();
            this.aspectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aspectAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deckeffectDataGridView = new System.Windows.Forms.DataGridView();
            this.deckId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deckDraws = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deckeffectLabel = new System.Windows.Forms.Label();
            this.aspectsLabel = new System.Windows.Forms.Label();
            this.alternativerecipesLabel = new System.Windows.Forms.Label();
            this.linkedLabel = new System.Windows.Forms.Label();
            this.mutationsListBox = new System.Windows.Forms.ListBox();
            this.mutationsLabel = new System.Windows.Forms.Label();
            this.alternativerecipesListBox = new System.Windows.Forms.ListBox();
            this.linkedListBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.requirementsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.extantreqsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablereqsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.effectsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspectsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deckeffectDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(12, 12);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 0;
            this.idTextBox.Text = "ID";
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(118, 12);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(100, 20);
            this.labelTextBox.TabIndex = 1;
            this.labelTextBox.Text = "Label";
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // actionIdTextBox
            // 
            this.actionIdTextBox.Location = new System.Drawing.Point(224, 12);
            this.actionIdTextBox.Name = "actionIdTextBox";
            this.actionIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.actionIdTextBox.TabIndex = 2;
            this.actionIdTextBox.Text = "actionId";
            this.actionIdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // endingTextBox
            // 
            this.endingTextBox.Location = new System.Drawing.Point(12, 38);
            this.endingTextBox.Name = "endingTextBox";
            this.endingTextBox.Size = new System.Drawing.Size(100, 20);
            this.endingTextBox.TabIndex = 3;
            this.endingTextBox.Text = "Ending";
            this.endingTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // burnimageTextBox
            // 
            this.burnimageTextBox.Location = new System.Drawing.Point(118, 38);
            this.burnimageTextBox.Name = "burnimageTextBox";
            this.burnimageTextBox.Size = new System.Drawing.Size(100, 20);
            this.burnimageTextBox.TabIndex = 4;
            this.burnimageTextBox.Text = "Burn Image";
            this.burnimageTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // startdescriptionTextBox
            // 
            this.startdescriptionTextBox.Location = new System.Drawing.Point(12, 64);
            this.startdescriptionTextBox.Multiline = true;
            this.startdescriptionTextBox.Name = "startdescriptionTextBox";
            this.startdescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.startdescriptionTextBox.Size = new System.Drawing.Size(732, 50);
            this.startdescriptionTextBox.TabIndex = 5;
            this.startdescriptionTextBox.Text = "Start Description";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 120);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(732, 50);
            this.descriptionTextBox.TabIndex = 6;
            this.descriptionTextBox.Text = "Description";
            // 
            // craftableCheckBox
            // 
            this.craftableCheckBox.AutoSize = true;
            this.craftableCheckBox.Enabled = false;
            this.craftableCheckBox.Location = new System.Drawing.Point(224, 40);
            this.craftableCheckBox.Name = "craftableCheckBox";
            this.craftableCheckBox.Size = new System.Drawing.Size(68, 17);
            this.craftableCheckBox.TabIndex = 7;
            this.craftableCheckBox.Text = "Craftable";
            this.craftableCheckBox.UseVisualStyleBackColor = true;
            // 
            // hintonlyCheckBox
            // 
            this.hintonlyCheckBox.AutoSize = true;
            this.hintonlyCheckBox.Enabled = false;
            this.hintonlyCheckBox.Location = new System.Drawing.Point(298, 40);
            this.hintonlyCheckBox.Name = "hintonlyCheckBox";
            this.hintonlyCheckBox.Size = new System.Drawing.Size(69, 17);
            this.hintonlyCheckBox.TabIndex = 8;
            this.hintonlyCheckBox.Text = "Hint Only";
            this.hintonlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // internaldeckLabel
            // 
            this.internaldeckLabel.AutoSize = true;
            this.internaldeckLabel.Location = new System.Drawing.Point(377, 15);
            this.internaldeckLabel.Name = "internaldeckLabel";
            this.internaldeckLabel.Size = new System.Drawing.Size(71, 13);
            this.internaldeckLabel.TabIndex = 9;
            this.internaldeckLabel.Text = "Internal Deck";
            // 
            // showInternalDeckButton
            // 
            this.showInternalDeckButton.Location = new System.Drawing.Point(373, 36);
            this.showInternalDeckButton.Name = "showInternalDeckButton";
            this.showInternalDeckButton.Size = new System.Drawing.Size(75, 23);
            this.showInternalDeckButton.TabIndex = 10;
            this.showInternalDeckButton.Text = "Show";
            this.showInternalDeckButton.UseVisualStyleBackColor = true;
            this.showInternalDeckButton.Click += new System.EventHandler(this.showInternalDeckButton_Click);
            // 
            // requirementsLabel
            // 
            this.requirementsLabel.AutoSize = true;
            this.requirementsLabel.Location = new System.Drawing.Point(99, 173);
            this.requirementsLabel.Name = "requirementsLabel";
            this.requirementsLabel.Size = new System.Drawing.Size(72, 13);
            this.requirementsLabel.TabIndex = 11;
            this.requirementsLabel.Text = "Requirements";
            // 
            // requirementsDataGridView
            // 
            this.requirementsDataGridView.AllowUserToAddRows = false;
            this.requirementsDataGridView.AllowUserToDeleteRows = false;
            this.requirementsDataGridView.AllowUserToResizeColumns = false;
            this.requirementsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.requirementsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.elementId,
            this.amount});
            this.requirementsDataGridView.Location = new System.Drawing.Point(12, 189);
            this.requirementsDataGridView.Name = "requirementsDataGridView";
            this.requirementsDataGridView.ReadOnly = true;
            this.requirementsDataGridView.Size = new System.Drawing.Size(240, 150);
            this.requirementsDataGridView.TabIndex = 12;
            this.requirementsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.requirementsDataGridView_CellDoubleClick);
            // 
            // elementId
            // 
            this.elementId.HeaderText = "Element ID";
            this.elementId.Name = "elementId";
            this.elementId.ReadOnly = true;
            this.elementId.Width = 99;
            // 
            // amount
            // 
            this.amount.HeaderText = "Amount";
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            this.amount.Width = 98;
            // 
            // extantreqsDataGridView
            // 
            this.extantreqsDataGridView.AllowUserToAddRows = false;
            this.extantreqsDataGridView.AllowUserToDeleteRows = false;
            this.extantreqsDataGridView.AllowUserToResizeColumns = false;
            this.extantreqsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.extantreqsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.extantElementId,
            this.extantAmount});
            this.extantreqsDataGridView.Location = new System.Drawing.Point(258, 189);
            this.extantreqsDataGridView.Name = "extantreqsDataGridView";
            this.extantreqsDataGridView.ReadOnly = true;
            this.extantreqsDataGridView.Size = new System.Drawing.Size(240, 150);
            this.extantreqsDataGridView.TabIndex = 13;
            this.extantreqsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.extantreqsDataGridView_CellDoubleClick);
            // 
            // extantElementId
            // 
            this.extantElementId.HeaderText = "Element ID";
            this.extantElementId.Name = "extantElementId";
            this.extantElementId.ReadOnly = true;
            this.extantElementId.Width = 99;
            // 
            // extantAmount
            // 
            this.extantAmount.HeaderText = "Amount";
            this.extantAmount.Name = "extantAmount";
            this.extantAmount.ReadOnly = true;
            this.extantAmount.Width = 98;
            // 
            // extantrequirementsLabel
            // 
            this.extantrequirementsLabel.AutoSize = true;
            this.extantrequirementsLabel.Location = new System.Drawing.Point(326, 173);
            this.extantrequirementsLabel.Name = "extantrequirementsLabel";
            this.extantrequirementsLabel.Size = new System.Drawing.Size(105, 13);
            this.extantrequirementsLabel.TabIndex = 14;
            this.extantrequirementsLabel.Text = "Extant Requirements";
            // 
            // tablereqsDataGridView
            // 
            this.tablereqsDataGridView.AllowUserToAddRows = false;
            this.tablereqsDataGridView.AllowUserToDeleteRows = false;
            this.tablereqsDataGridView.AllowUserToResizeColumns = false;
            this.tablereqsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablereqsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tableElementId,
            this.tableAmount});
            this.tablereqsDataGridView.Location = new System.Drawing.Point(504, 189);
            this.tablereqsDataGridView.Name = "tablereqsDataGridView";
            this.tablereqsDataGridView.ReadOnly = true;
            this.tablereqsDataGridView.Size = new System.Drawing.Size(240, 150);
            this.tablereqsDataGridView.TabIndex = 15;
            this.tablereqsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablereqsDataGridView_CellDoubleClick);
            // 
            // tableElementId
            // 
            this.tableElementId.HeaderText = "Element ID";
            this.tableElementId.Name = "tableElementId";
            this.tableElementId.ReadOnly = true;
            this.tableElementId.Width = 99;
            // 
            // tableAmount
            // 
            this.tableAmount.HeaderText = "Amount";
            this.tableAmount.Name = "tableAmount";
            this.tableAmount.ReadOnly = true;
            this.tableAmount.Width = 98;
            // 
            // tablerequirementsLabel
            // 
            this.tablerequirementsLabel.AutoSize = true;
            this.tablerequirementsLabel.Location = new System.Drawing.Point(576, 173);
            this.tablerequirementsLabel.Name = "tablerequirementsLabel";
            this.tablerequirementsLabel.Size = new System.Drawing.Size(102, 13);
            this.tablerequirementsLabel.TabIndex = 16;
            this.tablerequirementsLabel.Text = "Table Requirements";
            // 
            // slotLabel
            // 
            this.slotLabel.AutoSize = true;
            this.slotLabel.Location = new System.Drawing.Point(473, 15);
            this.slotLabel.Name = "slotLabel";
            this.slotLabel.Size = new System.Drawing.Size(25, 13);
            this.slotLabel.TabIndex = 17;
            this.slotLabel.Text = "Slot";
            // 
            // showSlotButton
            // 
            this.showSlotButton.Location = new System.Drawing.Point(454, 36);
            this.showSlotButton.Name = "showSlotButton";
            this.showSlotButton.Size = new System.Drawing.Size(75, 23);
            this.showSlotButton.TabIndex = 18;
            this.showSlotButton.Text = "Show";
            this.showSlotButton.UseVisualStyleBackColor = true;
            this.showSlotButton.Click += new System.EventHandler(this.showSlotButton_Click);
            // 
            // effectsDataGridView
            // 
            this.effectsDataGridView.AllowUserToAddRows = false;
            this.effectsDataGridView.AllowUserToDeleteRows = false;
            this.effectsDataGridView.AllowUserToResizeColumns = false;
            this.effectsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.effectsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.effectsElementId,
            this.effectsAmount});
            this.effectsDataGridView.Location = new System.Drawing.Point(12, 358);
            this.effectsDataGridView.Name = "effectsDataGridView";
            this.effectsDataGridView.ReadOnly = true;
            this.effectsDataGridView.Size = new System.Drawing.Size(240, 150);
            this.effectsDataGridView.TabIndex = 19;
            this.effectsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.effectsDataGridView_CellDoubleClick);
            // 
            // effectsElementId
            // 
            this.effectsElementId.HeaderText = "Element ID";
            this.effectsElementId.Name = "effectsElementId";
            this.effectsElementId.ReadOnly = true;
            this.effectsElementId.Width = 99;
            // 
            // effectsAmount
            // 
            this.effectsAmount.HeaderText = "Amount";
            this.effectsAmount.Name = "effectsAmount";
            this.effectsAmount.ReadOnly = true;
            this.effectsAmount.Width = 98;
            // 
            // effectsLabel
            // 
            this.effectsLabel.AutoSize = true;
            this.effectsLabel.Location = new System.Drawing.Point(99, 342);
            this.effectsLabel.Name = "effectsLabel";
            this.effectsLabel.Size = new System.Drawing.Size(40, 13);
            this.effectsLabel.TabIndex = 20;
            this.effectsLabel.Text = "Effects";
            // 
            // aspectsDataGridView
            // 
            this.aspectsDataGridView.AllowUserToAddRows = false;
            this.aspectsDataGridView.AllowUserToDeleteRows = false;
            this.aspectsDataGridView.AllowUserToResizeColumns = false;
            this.aspectsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.aspectsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aspectId,
            this.aspectAmount});
            this.aspectsDataGridView.Location = new System.Drawing.Point(258, 358);
            this.aspectsDataGridView.Name = "aspectsDataGridView";
            this.aspectsDataGridView.ReadOnly = true;
            this.aspectsDataGridView.Size = new System.Drawing.Size(240, 150);
            this.aspectsDataGridView.TabIndex = 21;
            this.aspectsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.aspectsDataGridView_CellDoubleClick);
            // 
            // aspectId
            // 
            this.aspectId.HeaderText = "Aspect ID";
            this.aspectId.Name = "aspectId";
            this.aspectId.ReadOnly = true;
            this.aspectId.Width = 99;
            // 
            // aspectAmount
            // 
            this.aspectAmount.HeaderText = "Amount";
            this.aspectAmount.Name = "aspectAmount";
            this.aspectAmount.ReadOnly = true;
            this.aspectAmount.Width = 98;
            // 
            // deckeffectDataGridView
            // 
            this.deckeffectDataGridView.AllowUserToAddRows = false;
            this.deckeffectDataGridView.AllowUserToDeleteRows = false;
            this.deckeffectDataGridView.AllowUserToResizeColumns = false;
            this.deckeffectDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.deckeffectDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.deckId,
            this.deckDraws});
            this.deckeffectDataGridView.Location = new System.Drawing.Point(504, 358);
            this.deckeffectDataGridView.Name = "deckeffectDataGridView";
            this.deckeffectDataGridView.ReadOnly = true;
            this.deckeffectDataGridView.Size = new System.Drawing.Size(240, 150);
            this.deckeffectDataGridView.TabIndex = 22;
            this.deckeffectDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.deckeffectDataGridView_CellDoubleClick);
            // 
            // deckId
            // 
            this.deckId.HeaderText = "Deck ID";
            this.deckId.Name = "deckId";
            this.deckId.ReadOnly = true;
            this.deckId.Width = 99;
            // 
            // deckDraws
            // 
            this.deckDraws.HeaderText = "Amount";
            this.deckDraws.Name = "deckDraws";
            this.deckDraws.ReadOnly = true;
            this.deckDraws.Width = 98;
            // 
            // deckeffectLabel
            // 
            this.deckeffectLabel.AutoSize = true;
            this.deckeffectLabel.Location = new System.Drawing.Point(595, 342);
            this.deckeffectLabel.Name = "deckeffectLabel";
            this.deckeffectLabel.Size = new System.Drawing.Size(64, 13);
            this.deckeffectLabel.TabIndex = 23;
            this.deckeffectLabel.Text = "Deck Effect";
            // 
            // aspectsLabel
            // 
            this.aspectsLabel.AutoSize = true;
            this.aspectsLabel.Location = new System.Drawing.Point(346, 342);
            this.aspectsLabel.Name = "aspectsLabel";
            this.aspectsLabel.Size = new System.Drawing.Size(45, 13);
            this.aspectsLabel.TabIndex = 24;
            this.aspectsLabel.Text = "Aspects";
            // 
            // alternativerecipesLabel
            // 
            this.alternativerecipesLabel.AutoSize = true;
            this.alternativerecipesLabel.Location = new System.Drawing.Point(72, 511);
            this.alternativerecipesLabel.Name = "alternativerecipesLabel";
            this.alternativerecipesLabel.Size = new System.Drawing.Size(99, 13);
            this.alternativerecipesLabel.TabIndex = 26;
            this.alternativerecipesLabel.Text = "Alternative Recipes";
            // 
            // linkedLabel
            // 
            this.linkedLabel.AutoSize = true;
            this.linkedLabel.Location = new System.Drawing.Point(326, 511);
            this.linkedLabel.Name = "linkedLabel";
            this.linkedLabel.Size = new System.Drawing.Size(81, 13);
            this.linkedLabel.TabIndex = 27;
            this.linkedLabel.Text = "Linked Recipes";
            // 
            // mutationsListBox
            // 
            this.mutationsListBox.FormattingEnabled = true;
            this.mutationsListBox.Location = new System.Drawing.Point(504, 527);
            this.mutationsListBox.Name = "mutationsListBox";
            this.mutationsListBox.ScrollAlwaysVisible = true;
            this.mutationsListBox.Size = new System.Drawing.Size(240, 147);
            this.mutationsListBox.TabIndex = 29;
            this.mutationsListBox.DoubleClick += new System.EventHandler(this.mutationsListBox_DoubleClick);
            // 
            // mutationsLabel
            // 
            this.mutationsLabel.AutoSize = true;
            this.mutationsLabel.Location = new System.Drawing.Point(595, 511);
            this.mutationsLabel.Name = "mutationsLabel";
            this.mutationsLabel.Size = new System.Drawing.Size(53, 13);
            this.mutationsLabel.TabIndex = 30;
            this.mutationsLabel.Text = "Mutations";
            // 
            // alternativerecipesListBox
            // 
            this.alternativerecipesListBox.FormattingEnabled = true;
            this.alternativerecipesListBox.Location = new System.Drawing.Point(12, 527);
            this.alternativerecipesListBox.Name = "alternativerecipesListBox";
            this.alternativerecipesListBox.ScrollAlwaysVisible = true;
            this.alternativerecipesListBox.Size = new System.Drawing.Size(240, 147);
            this.alternativerecipesListBox.TabIndex = 31;
            this.alternativerecipesListBox.DoubleClick += new System.EventHandler(this.alternativerecipesListBox_DoubleClick);
            // 
            // linkedListBox
            // 
            this.linkedListBox.FormattingEnabled = true;
            this.linkedListBox.Location = new System.Drawing.Point(258, 527);
            this.linkedListBox.Name = "linkedListBox";
            this.linkedListBox.ScrollAlwaysVisible = true;
            this.linkedListBox.Size = new System.Drawing.Size(240, 147);
            this.linkedListBox.TabIndex = 32;
            this.linkedListBox.DoubleClick += new System.EventHandler(this.linkedListBox_DoubleClick);
            // 
            // RecipeViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 684);
            this.Controls.Add(this.linkedListBox);
            this.Controls.Add(this.alternativerecipesListBox);
            this.Controls.Add(this.mutationsLabel);
            this.Controls.Add(this.mutationsListBox);
            this.Controls.Add(this.linkedLabel);
            this.Controls.Add(this.alternativerecipesLabel);
            this.Controls.Add(this.aspectsLabel);
            this.Controls.Add(this.deckeffectLabel);
            this.Controls.Add(this.deckeffectDataGridView);
            this.Controls.Add(this.aspectsDataGridView);
            this.Controls.Add(this.effectsLabel);
            this.Controls.Add(this.effectsDataGridView);
            this.Controls.Add(this.showSlotButton);
            this.Controls.Add(this.slotLabel);
            this.Controls.Add(this.tablerequirementsLabel);
            this.Controls.Add(this.tablereqsDataGridView);
            this.Controls.Add(this.extantrequirementsLabel);
            this.Controls.Add(this.extantreqsDataGridView);
            this.Controls.Add(this.requirementsDataGridView);
            this.Controls.Add(this.requirementsLabel);
            this.Controls.Add(this.showInternalDeckButton);
            this.Controls.Add(this.internaldeckLabel);
            this.Controls.Add(this.hintonlyCheckBox);
            this.Controls.Add(this.craftableCheckBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.startdescriptionTextBox);
            this.Controls.Add(this.burnimageTextBox);
            this.Controls.Add(this.endingTextBox);
            this.Controls.Add(this.actionIdTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Name = "RecipeViewer";
            this.Text = "RecipeViewer";
            ((System.ComponentModel.ISupportInitialize)(this.requirementsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.extantreqsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablereqsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.effectsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspectsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deckeffectDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.TextBox actionIdTextBox;
        private System.Windows.Forms.TextBox endingTextBox;
        private System.Windows.Forms.TextBox burnimageTextBox;
        private System.Windows.Forms.TextBox startdescriptionTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.CheckBox craftableCheckBox;
        private System.Windows.Forms.CheckBox hintonlyCheckBox;
        private System.Windows.Forms.Label internaldeckLabel;
        private System.Windows.Forms.Button showInternalDeckButton;
        private System.Windows.Forms.Label requirementsLabel;
        private System.Windows.Forms.DataGridView requirementsDataGridView;
        private System.Windows.Forms.DataGridView extantreqsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn extantElementId;
        private System.Windows.Forms.DataGridViewTextBoxColumn extantAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn elementId;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.Label extantrequirementsLabel;
        private System.Windows.Forms.DataGridView tablereqsDataGridView;
        private System.Windows.Forms.Label tablerequirementsLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableElementId;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableAmount;
        private System.Windows.Forms.Label slotLabel;
        private System.Windows.Forms.Button showSlotButton;
        private System.Windows.Forms.DataGridView effectsDataGridView;
        private System.Windows.Forms.Label effectsLabel;
        private System.Windows.Forms.DataGridView aspectsDataGridView;
        private System.Windows.Forms.DataGridView deckeffectDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn effectsElementId;
        private System.Windows.Forms.DataGridViewTextBoxColumn effectsAmount;
        private System.Windows.Forms.Label deckeffectLabel;
        private System.Windows.Forms.Label aspectsLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn aspectId;
        private System.Windows.Forms.DataGridViewTextBoxColumn aspectAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn deckId;
        private System.Windows.Forms.DataGridViewTextBoxColumn deckDraws;
        private System.Windows.Forms.Label alternativerecipesLabel;
        private System.Windows.Forms.Label linkedLabel;
        private System.Windows.Forms.ListBox mutationsListBox;
        private System.Windows.Forms.Label mutationsLabel;
        private System.Windows.Forms.ListBox alternativerecipesListBox;
        private System.Windows.Forms.ListBox linkedListBox;
    }
}