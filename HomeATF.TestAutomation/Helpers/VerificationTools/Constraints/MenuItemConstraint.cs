using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeATF.Appium;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace HomeATF.TestAutomation
{
    public class MenuItemConstraint : NUnit.Framework.Constraints.Constraint
    {
        private readonly ExpectedMenuItemElement expectedElement;

        public MenuItemConstraint(ExpectedMenuItemElement expected)
        {
            this.expectedElement = expected;
        }

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            ConstraintStatus status = ConstraintStatus.Success;

            if (actual == null)
            {
                this.Description = $"Actual element is with label {this.expectedElement.Label} is null or not found";
                status = ConstraintStatus.Failure;
                return new ConstraintResult(this, actual, status);
            }
            var actualElement = actual as Element;

            if (!VerificationTools.VerifyMenuItemLayout(actualElement, this.expectedElement, out string errorDesc))
            {
                this.Description += errorDesc;
                status = ConstraintStatus.Failure;
            }

            return new ConstraintResult(this, actual, status);
        }
    }
}
