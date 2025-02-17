﻿using VertiGIS.Mobile.Workflow.Core;

namespace VertiGIS.Mobile.Samples.Samples.Workflow.CustomFormComponent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomFormComponent : ContentComponent
    {
        public CustomFormComponent(VertiGIS.Workflow.Runtime.Definition.Forms.Element element, string name)
            : base(element, name)
        {
            InitializeComponent();
        }
    }
}
