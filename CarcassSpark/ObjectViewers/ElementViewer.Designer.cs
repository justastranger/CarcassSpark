namespace CarcassSpark.ObjectViewers
{
    partial class ElementViewer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ElementViewer));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.iconTextBox = new System.Windows.Forms.TextBox();
            this.decayToTextBox = new System.Windows.Forms.TextBox();
            this.uniqueCheckBox = new System.Windows.Forms.CheckBox();
            this.uniquenessgroupTextBox = new System.Windows.Forms.TextBox();
            this.xtriggersLabel = new System.Windows.Forms.Label();
            this.slotsLabel = new System.Windows.Forms.Label();
            this.aspectsDataGridView = new System.Windows.Forms.DataGridView();
            this.aspectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyOperationContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setAsExtendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAsRemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.aspectsLabel = new System.Windows.Forms.Label();
            this.lifetimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lifetimeLabel = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.labelLabel = new System.Windows.Forms.Label();
            this.iconLabel = new System.Windows.Forms.Label();
            this.uniquenessgroupLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.decayToLabel = new System.Windows.Forms.Label();
            this.addSlotButton = new System.Windows.Forms.Button();
            this.removeSlotButton = new System.Windows.Forms.Button();
            this.resaturateCheckBox = new System.Windows.Forms.CheckBox();
            this.slotsListView = new System.Windows.Forms.ListView();
            this.xtriggersListView = new System.Windows.Forms.ListView();
            this.newXTriggerButton = new System.Windows.Forms.Button();
            this.deleteXTriggerButton = new System.Windows.Forms.Button();
            this.commentsLabel = new System.Windows.Forms.Label();
            this.commentsTextBox = new System.Windows.Forms.TextBox();
            this.inheritsLabel = new System.Windows.Forms.Label();
            this.inheritsTextBox = new System.Windows.Forms.TextBox();
            this.deletedCheckBox = new System.Windows.Forms.CheckBox();
            this.extendsTextBox = new System.Windows.Forms.TextBox();
            this.extendsLabel = new System.Windows.Forms.Label();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.verbIconTextBox = new System.Windows.Forms.TextBox();
            this.extendXTriggerButton = new System.Windows.Forms.Button();
            this.verbIconLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspectsDataGridView)).BeginInit();
            this.propertyOperationContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lifetimeNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(149, 25);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 1;
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.idTextBox, "Internal name of element.");
            this.idTextBox.TextChanged += new System.EventHandler(this.IdTextBox_TextChanged);
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(149, 64);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(100, 20);
            this.labelTextBox.TabIndex = 2;
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.labelTextBox, "Name of element to be displayed to player.");
            this.labelTextBox.TextChanged += new System.EventHandler(this.LabelTextBox_TextChanged);
            // 
            // iconTextBox
            // 
            this.iconTextBox.Location = new System.Drawing.Point(149, 103);
            this.iconTextBox.Name = "iconTextBox";
            this.iconTextBox.Size = new System.Drawing.Size(100, 20);
            this.iconTextBox.TabIndex = 3;
            this.iconTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.iconTextBox, "Filename, minus extension, of the image from \"images/elements\" to be displayed fo" +
        "r this element.\r\nWhen not present, the ID is used instead.");
            this.iconTextBox.TextChanged += new System.EventHandler(this.IconTextBox_TextChanged);
            // 
            // decayToTextBox
            // 
            this.decayToTextBox.Location = new System.Drawing.Point(149, 181);
            this.decayToTextBox.Name = "decayToTextBox";
            this.decayToTextBox.Size = new System.Drawing.Size(100, 20);
            this.decayToTextBox.TabIndex = 6;
            this.decayToTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.decayToTextBox, "ID of element to transform into once lifetime timer hits 0. If blank, the card is" +
        " destroyed when it decays.");
            this.decayToTextBox.TextChanged += new System.EventHandler(this.DecayToTextBox_TextChanged);
            // 
            // uniqueCheckBox
            // 
            this.uniqueCheckBox.AutoSize = true;
            this.uniqueCheckBox.Enabled = false;
            this.uniqueCheckBox.Location = new System.Drawing.Point(15, 196);
            this.uniqueCheckBox.Name = "uniqueCheckBox";
            this.uniqueCheckBox.Size = new System.Drawing.Size(60, 17);
            this.uniqueCheckBox.TabIndex = 7;
            this.uniqueCheckBox.Text = "Unique";
            this.uniqueCheckBox.ThreeState = true;
            this.ToolTip.SetToolTip(this.uniqueCheckBox, "When true, only one copy of the card is allowed at a time. Spawning or receiving " +
        "a new copy will purge the old copy.");
            this.uniqueCheckBox.UseVisualStyleBackColor = true;
            this.uniqueCheckBox.CheckStateChanged += new System.EventHandler(this.UniqueCheckBox_CheckStateChanged);
            // 
            // uniquenessgroupTextBox
            // 
            this.uniquenessgroupTextBox.Location = new System.Drawing.Point(149, 142);
            this.uniquenessgroupTextBox.Name = "uniquenessgroupTextBox";
            this.uniquenessgroupTextBox.Size = new System.Drawing.Size(100, 20);
            this.uniquenessgroupTextBox.TabIndex = 8;
            this.uniquenessgroupTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.uniquenessgroupTextBox, resources.GetString("uniquenessgroupTextBox.ToolTip"));
            this.uniquenessgroupTextBox.TextChanged += new System.EventHandler(this.UniquenessgroupTextBox_TextChanged);
            // 
            // xtriggersLabel
            // 
            this.xtriggersLabel.AutoSize = true;
            this.xtriggersLabel.Location = new System.Drawing.Point(330, 9);
            this.xtriggersLabel.Name = "xtriggersLabel";
            this.xtriggersLabel.Size = new System.Drawing.Size(52, 13);
            this.xtriggersLabel.TabIndex = 10;
            this.xtriggersLabel.Text = "XTriggers";
            // 
            // slotsLabel
            // 
            this.slotsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.slotsLabel.AutoSize = true;
            this.slotsLabel.Location = new System.Drawing.Point(506, 204);
            this.slotsLabel.Name = "slotsLabel";
            this.slotsLabel.Size = new System.Drawing.Size(30, 13);
            this.slotsLabel.TabIndex = 11;
            this.slotsLabel.Text = "Slots";
            // 
            // aspectsDataGridView
            // 
            this.aspectsDataGridView.AllowUserToResizeColumns = false;
            this.aspectsDataGridView.AllowUserToResizeRows = false;
            this.aspectsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.aspectsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.aspectsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aspectId,
            this.amount});
            this.aspectsDataGridView.ContextMenuStrip = this.propertyOperationContextMenuStrip;
            this.aspectsDataGridView.Location = new System.Drawing.Point(509, 25);
            this.aspectsDataGridView.Name = "aspectsDataGridView";
            this.aspectsDataGridView.Size = new System.Drawing.Size(248, 176);
            this.aspectsDataGridView.TabIndex = 13;
            this.ToolTip.SetToolTip(this.aspectsDataGridView, "The aspects of the element. Can be the ID of either an aspect or another element." +
        "");
            this.aspectsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AspectsDataGridView_CellDoubleClick);
            this.aspectsDataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.AspectsDataGridView_UserDeletedRow);
            // 
            // aspectId
            // 
            this.aspectId.HeaderText = "Aspect ID";
            this.aspectId.Name = "aspectId";
            this.aspectId.Width = 103;
            // 
            // amount
            // 
            this.amount.HeaderText = "Amount";
            this.amount.Name = "amount";
            this.amount.Width = 102;
            // 
            // propertyOperationContextMenuStrip
            // 
            this.propertyOperationContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setAsExtendToolStripMenuItem,
            this.setAsRemoveToolStripMenuItem});
            this.propertyOperationContextMenuStrip.Name = "propertyOperationContextMenuStrip";
            this.propertyOperationContextMenuStrip.ShowImageMargin = false;
            this.propertyOperationContextMenuStrip.Size = new System.Drawing.Size(126, 48);
            // 
            // setAsExtendToolStripMenuItem
            // 
            this.setAsExtendToolStripMenuItem.Name = "setAsExtendToolStripMenuItem";
            this.setAsExtendToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.setAsExtendToolStripMenuItem.Text = "Set as Extend";
            this.setAsExtendToolStripMenuItem.Click += new System.EventHandler(this.SetAsExtendToolStripMenuItem_Click);
            // 
            // setAsRemoveToolStripMenuItem
            // 
            this.setAsRemoveToolStripMenuItem.Name = "setAsRemoveToolStripMenuItem";
            this.setAsRemoveToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.setAsRemoveToolStripMenuItem.Text = "Set as Remove";
            this.setAsRemoveToolStripMenuItem.Click += new System.EventHandler(this.SetAsRemoveToolStripMenuItem_Click);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.AcceptsReturn = true;
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 248);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(491, 71);
            this.descriptionTextBox.TabIndex = 15;
            this.ToolTip.SetToolTip(this.descriptionTextBox, "Description shown in the element\'s details panel.");
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.DescriptionTextBox_TextChanged);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 377);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 16;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(682, 377);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 17;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // aspectsLabel
            // 
            this.aspectsLabel.AutoSize = true;
            this.aspectsLabel.Location = new System.Drawing.Point(612, 9);
            this.aspectsLabel.Name = "aspectsLabel";
            this.aspectsLabel.Size = new System.Drawing.Size(45, 13);
            this.aspectsLabel.TabIndex = 18;
            this.aspectsLabel.Text = "Aspects";
            // 
            // lifetimeNumericUpDown
            // 
            this.lifetimeNumericUpDown.Location = new System.Drawing.Point(15, 170);
            this.lifetimeNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.lifetimeNumericUpDown.Name = "lifetimeNumericUpDown";
            this.lifetimeNumericUpDown.Size = new System.Drawing.Size(125, 20);
            this.lifetimeNumericUpDown.TabIndex = 19;
            this.ToolTip.SetToolTip(this.lifetimeNumericUpDown, "How long, in seconds, until the card containing this element should decay.");
            this.lifetimeNumericUpDown.ValueChanged += new System.EventHandler(this.LifetimeNumericUpDown_ValueChanged);
            // 
            // lifetimeLabel
            // 
            this.lifetimeLabel.AutoSize = true;
            this.lifetimeLabel.Location = new System.Drawing.Point(12, 154);
            this.lifetimeLabel.Name = "lifetimeLabel";
            this.lifetimeLabel.Size = new System.Drawing.Size(43, 13);
            this.lifetimeLabel.TabIndex = 20;
            this.lifetimeLabel.Text = "Lifetime";
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(146, 9);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(59, 13);
            this.idLabel.TabIndex = 23;
            this.idLabel.Text = "Element ID";
            // 
            // labelLabel
            // 
            this.labelLabel.AutoSize = true;
            this.labelLabel.Location = new System.Drawing.Point(146, 48);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(33, 13);
            this.labelLabel.TabIndex = 24;
            this.labelLabel.Text = "Label";
            // 
            // iconLabel
            // 
            this.iconLabel.AutoSize = true;
            this.iconLabel.Location = new System.Drawing.Point(146, 87);
            this.iconLabel.Name = "iconLabel";
            this.iconLabel.Size = new System.Drawing.Size(28, 13);
            this.iconLabel.TabIndex = 25;
            this.iconLabel.Text = "Icon";
            // 
            // uniquenessgroupLabel
            // 
            this.uniquenessgroupLabel.AutoSize = true;
            this.uniquenessgroupLabel.Location = new System.Drawing.Point(146, 126);
            this.uniquenessgroupLabel.Name = "uniquenessgroupLabel";
            this.uniquenessgroupLabel.Size = new System.Drawing.Size(95, 13);
            this.uniquenessgroupLabel.TabIndex = 26;
            this.uniquenessgroupLabel.Text = "Uniqueness Group";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(12, 232);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 27;
            this.descriptionLabel.Text = "Description";
            // 
            // decayToLabel
            // 
            this.decayToLabel.AutoSize = true;
            this.decayToLabel.Location = new System.Drawing.Point(146, 165);
            this.decayToLabel.Name = "decayToLabel";
            this.decayToLabel.Size = new System.Drawing.Size(54, 13);
            this.decayToLabel.TabIndex = 29;
            this.decayToLabel.Text = "Decay To";
            // 
            // addSlotButton
            // 
            this.addSlotButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addSlotButton.Location = new System.Drawing.Point(509, 377);
            this.addSlotButton.Name = "addSlotButton";
            this.addSlotButton.Size = new System.Drawing.Size(75, 23);
            this.addSlotButton.TabIndex = 30;
            this.addSlotButton.Text = "Add Slot";
            this.addSlotButton.UseVisualStyleBackColor = true;
            this.addSlotButton.Click += new System.EventHandler(this.AddSlotButton_Click);
            // 
            // removeSlotButton
            // 
            this.removeSlotButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeSlotButton.Location = new System.Drawing.Point(590, 377);
            this.removeSlotButton.Name = "removeSlotButton";
            this.removeSlotButton.Size = new System.Drawing.Size(86, 23);
            this.removeSlotButton.TabIndex = 31;
            this.removeSlotButton.Text = "Remove Slot";
            this.removeSlotButton.UseVisualStyleBackColor = true;
            this.removeSlotButton.Click += new System.EventHandler(this.RemoveSlotButton_Click);
            // 
            // resaturateCheckBox
            // 
            this.resaturateCheckBox.AutoSize = true;
            this.resaturateCheckBox.Location = new System.Drawing.Point(15, 212);
            this.resaturateCheckBox.Name = "resaturateCheckBox";
            this.resaturateCheckBox.Size = new System.Drawing.Size(78, 17);
            this.resaturateCheckBox.TabIndex = 32;
            this.resaturateCheckBox.Text = "Resaturate";
            this.resaturateCheckBox.ThreeState = true;
            this.ToolTip.SetToolTip(this.resaturateCheckBox, "Inverts the grayscale mechanic from turning grayer the closer it is to decaying t" +
        "o gaining more color the closer it gets to decaying.");
            this.resaturateCheckBox.UseVisualStyleBackColor = true;
            this.resaturateCheckBox.CheckStateChanged += new System.EventHandler(this.ResaturateCheckBox_CheckStateChanged);
            // 
            // slotsListView
            // 
            this.slotsListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.slotsListView.HideSelection = false;
            this.slotsListView.Location = new System.Drawing.Point(509, 220);
            this.slotsListView.MultiSelect = false;
            this.slotsListView.Name = "slotsListView";
            this.slotsListView.Size = new System.Drawing.Size(248, 151);
            this.slotsListView.TabIndex = 33;
            this.ToolTip.SetToolTip(this.slotsListView, "Specifies the slots that appear when you add the element to various verbs.");
            this.slotsListView.UseCompatibleStateImageBehavior = false;
            this.slotsListView.View = System.Windows.Forms.View.List;
            this.slotsListView.DoubleClick += new System.EventHandler(this.SlotsListView_DoubleClick);
            // 
            // xtriggersListView
            // 
            this.xtriggersListView.HideSelection = false;
            this.xtriggersListView.Location = new System.Drawing.Point(258, 26);
            this.xtriggersListView.MultiSelect = false;
            this.xtriggersListView.Name = "xtriggersListView";
            this.xtriggersListView.Size = new System.Drawing.Size(248, 97);
            this.xtriggersListView.TabIndex = 34;
            this.ToolTip.SetToolTip(this.xtriggersListView, resources.GetString("xtriggersListView.ToolTip"));
            this.xtriggersListView.UseCompatibleStateImageBehavior = false;
            this.xtriggersListView.View = System.Windows.Forms.View.List;
            this.xtriggersListView.DoubleClick += new System.EventHandler(this.XtriggersListView_DoubleClick);
            // 
            // newXTriggerButton
            // 
            this.newXTriggerButton.Location = new System.Drawing.Point(258, 128);
            this.newXTriggerButton.Name = "newXTriggerButton";
            this.newXTriggerButton.Size = new System.Drawing.Size(87, 23);
            this.newXTriggerButton.TabIndex = 35;
            this.newXTriggerButton.Text = "New XTrigger";
            this.newXTriggerButton.UseVisualStyleBackColor = true;
            this.newXTriggerButton.Click += new System.EventHandler(this.NewXTriggerButton_Click);
            // 
            // deleteXTriggerButton
            // 
            this.deleteXTriggerButton.Location = new System.Drawing.Point(351, 128);
            this.deleteXTriggerButton.Name = "deleteXTriggerButton";
            this.deleteXTriggerButton.Size = new System.Drawing.Size(90, 23);
            this.deleteXTriggerButton.TabIndex = 36;
            this.deleteXTriggerButton.Text = "Delete XTrigger";
            this.deleteXTriggerButton.UseVisualStyleBackColor = true;
            this.deleteXTriggerButton.Click += new System.EventHandler(this.DeleteXTriggerButton_Click);
            // 
            // commentsLabel
            // 
            this.commentsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.commentsLabel.AutoSize = true;
            this.commentsLabel.Location = new System.Drawing.Point(12, 322);
            this.commentsLabel.Name = "commentsLabel";
            this.commentsLabel.Size = new System.Drawing.Size(56, 13);
            this.commentsLabel.TabIndex = 37;
            this.commentsLabel.Text = "Comments";
            // 
            // commentsTextBox
            // 
            this.commentsTextBox.AcceptsReturn = true;
            this.commentsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.commentsTextBox.Location = new System.Drawing.Point(12, 338);
            this.commentsTextBox.Multiline = true;
            this.commentsTextBox.Name = "commentsTextBox";
            this.commentsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commentsTextBox.Size = new System.Drawing.Size(491, 33);
            this.commentsTextBox.TabIndex = 38;
            this.commentsTextBox.TextChanged += new System.EventHandler(this.CommentsTextBox_TextChanged);
            // 
            // inheritsLabel
            // 
            this.inheritsLabel.AutoSize = true;
            this.inheritsLabel.Location = new System.Drawing.Point(146, 204);
            this.inheritsLabel.Name = "inheritsLabel";
            this.inheritsLabel.Size = new System.Drawing.Size(41, 13);
            this.inheritsLabel.TabIndex = 39;
            this.inheritsLabel.Text = "Inherits";
            // 
            // inheritsTextBox
            // 
            this.inheritsTextBox.Location = new System.Drawing.Point(149, 220);
            this.inheritsTextBox.Name = "inheritsTextBox";
            this.inheritsTextBox.Size = new System.Drawing.Size(100, 20);
            this.inheritsTextBox.TabIndex = 40;
            this.ToolTip.SetToolTip(this.inheritsTextBox, "ID of an element to inherit properties from.\r\nFunctionality is being replaced by " +
        "the new way that the Extends property is being handled.");
            this.inheritsTextBox.TextChanged += new System.EventHandler(this.InheritsTextBox_TextChanged);
            // 
            // deletedCheckBox
            // 
            this.deletedCheckBox.AutoSize = true;
            this.deletedCheckBox.Location = new System.Drawing.Point(77, 196);
            this.deletedCheckBox.Name = "deletedCheckBox";
            this.deletedCheckBox.Size = new System.Drawing.Size(63, 17);
            this.deletedCheckBox.TabIndex = 41;
            this.deletedCheckBox.Text = "Deleted";
            this.deletedCheckBox.ThreeState = true;
            this.deletedCheckBox.UseVisualStyleBackColor = true;
            this.deletedCheckBox.CheckStateChanged += new System.EventHandler(this.DeletedCheckBox_CheckStateChanged);
            // 
            // extendsTextBox
            // 
            this.extendsTextBox.Location = new System.Drawing.Point(258, 220);
            this.extendsTextBox.Name = "extendsTextBox";
            this.extendsTextBox.Size = new System.Drawing.Size(124, 20);
            this.extendsTextBox.TabIndex = 42;
            this.ToolTip.SetToolTip(this.extendsTextBox, "Comma separated list of elements to inherit properties from.\r\n\"example1,example2," +
        "example3\"");
            this.extendsTextBox.TextChanged += new System.EventHandler(this.ExtendsTextBox_TextChanged);
            // 
            // extendsLabel
            // 
            this.extendsLabel.AutoSize = true;
            this.extendsLabel.Location = new System.Drawing.Point(255, 204);
            this.extendsLabel.Name = "extendsLabel";
            this.extendsLabel.Size = new System.Drawing.Size(45, 13);
            this.extendsLabel.TabIndex = 43;
            this.extendsLabel.Text = "Extends";
            // 
            // verbIconTextBox
            // 
            this.verbIconTextBox.Location = new System.Drawing.Point(388, 220);
            this.verbIconTextBox.Name = "verbIconTextBox";
            this.verbIconTextBox.Size = new System.Drawing.Size(112, 20);
            this.verbIconTextBox.TabIndex = 45;
            this.ToolTip.SetToolTip(this.verbIconTextBox, "Filename, minus extension, of the image from \"images/verbs\" to override the verb\'" +
        "s normal icon with when this element is present.");
            this.verbIconTextBox.TextChanged += new System.EventHandler(this.VerbIconTextBox_TextChanged);
            // 
            // extendXTriggerButton
            // 
            this.extendXTriggerButton.Location = new System.Drawing.Point(258, 157);
            this.extendXTriggerButton.Name = "extendXTriggerButton";
            this.extendXTriggerButton.Size = new System.Drawing.Size(95, 23);
            this.extendXTriggerButton.TabIndex = 44;
            this.extendXTriggerButton.Text = "Append XTrigger";
            this.extendXTriggerButton.UseVisualStyleBackColor = true;
            this.extendXTriggerButton.Click += new System.EventHandler(this.ExtendXTriggerButton_Click);
            // 
            // verbIconLabel
            // 
            this.verbIconLabel.AutoSize = true;
            this.verbIconLabel.Location = new System.Drawing.Point(388, 204);
            this.verbIconLabel.Name = "verbIconLabel";
            this.verbIconLabel.Size = new System.Drawing.Size(53, 13);
            this.verbIconLabel.TabIndex = 46;
            this.verbIconLabel.Text = "Verb Icon";
            // 
            // ElementViewer
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(769, 412);
            this.Controls.Add(this.verbIconLabel);
            this.Controls.Add(this.verbIconTextBox);
            this.Controls.Add(this.extendXTriggerButton);
            this.Controls.Add(this.extendsLabel);
            this.Controls.Add(this.extendsTextBox);
            this.Controls.Add(this.deletedCheckBox);
            this.Controls.Add(this.inheritsTextBox);
            this.Controls.Add(this.inheritsLabel);
            this.Controls.Add(this.commentsTextBox);
            this.Controls.Add(this.commentsLabel);
            this.Controls.Add(this.deleteXTriggerButton);
            this.Controls.Add(this.newXTriggerButton);
            this.Controls.Add(this.xtriggersListView);
            this.Controls.Add(this.slotsListView);
            this.Controls.Add(this.resaturateCheckBox);
            this.Controls.Add(this.removeSlotButton);
            this.Controls.Add(this.addSlotButton);
            this.Controls.Add(this.decayToLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.uniquenessgroupLabel);
            this.Controls.Add(this.iconLabel);
            this.Controls.Add(this.labelLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.lifetimeLabel);
            this.Controls.Add(this.lifetimeNumericUpDown);
            this.Controls.Add(this.aspectsLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.aspectsDataGridView);
            this.Controls.Add(this.slotsLabel);
            this.Controls.Add(this.xtriggersLabel);
            this.Controls.Add(this.uniquenessgroupTextBox);
            this.Controls.Add(this.uniqueCheckBox);
            this.Controls.Add(this.decayToTextBox);
            this.Controls.Add(this.iconTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(785, 451);
            this.Name = "ElementViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Element Viewer";
            this.Shown += new System.EventHandler(this.ElementViewer_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aspectsDataGridView)).EndInit();
            this.propertyOperationContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lifetimeNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.TextBox iconTextBox;
        private System.Windows.Forms.TextBox decayToTextBox;
        private System.Windows.Forms.CheckBox uniqueCheckBox;
        private System.Windows.Forms.TextBox uniquenessgroupTextBox;
        private System.Windows.Forms.Label xtriggersLabel;
        private System.Windows.Forms.Label slotsLabel;
        private System.Windows.Forms.DataGridView aspectsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn aspectId;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label aspectsLabel;
        private System.Windows.Forms.NumericUpDown lifetimeNumericUpDown;
        private System.Windows.Forms.Label lifetimeLabel;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.Label iconLabel;
        private System.Windows.Forms.Label uniquenessgroupLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label decayToLabel;
        private System.Windows.Forms.Button addSlotButton;
        private System.Windows.Forms.Button removeSlotButton;
        private System.Windows.Forms.CheckBox resaturateCheckBox;
        private System.Windows.Forms.ListView slotsListView;
        private System.Windows.Forms.ListView xtriggersListView;
        private System.Windows.Forms.Button newXTriggerButton;
        private System.Windows.Forms.Button deleteXTriggerButton;
        private System.Windows.Forms.ContextMenuStrip propertyOperationContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem setAsExtendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAsRemoveToolStripMenuItem;
        private System.Windows.Forms.Label commentsLabel;
        private System.Windows.Forms.TextBox commentsTextBox;
        private System.Windows.Forms.Label inheritsLabel;
        private System.Windows.Forms.TextBox inheritsTextBox;
        private System.Windows.Forms.CheckBox deletedCheckBox;
        private System.Windows.Forms.TextBox extendsTextBox;
        private System.Windows.Forms.Label extendsLabel;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.Button extendXTriggerButton;
        private System.Windows.Forms.TextBox verbIconTextBox;
        private System.Windows.Forms.Label verbIconLabel;
    }
}