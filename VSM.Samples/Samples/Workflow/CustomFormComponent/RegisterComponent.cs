﻿using VertiGIS.Workflow.Runtime;
using VertiGIS.Workflow.Runtime.Activities.App;

namespace VertiGIS.Mobile.Samples.Samples.Workflow.CustomFormComponent
{
    class RegisterComponent : RegisterCustomFormElementBase
    {
        public const string Action = "CustomComponent";

        public override Task<IDictionary<string, object>> Execute(IDictionary<string, object> inputs, IActivityContext context)
        {
            IDictionary<string, object> result = new Dictionary<string, object>();
            Register("CustomComponent", typeof(CustomFormComponent), context);
            return Task.FromResult(result);
        }
    }
}
