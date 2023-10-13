using MultiSelectTreeView.Test.Model.Helper;
using System.Windows.Automation;

namespace MultiSelectTreeView.Test.Model {
    class BindingTrees {
        public BindingTrees(TreeApplication app) {
            this.LeftTree = new Tree(app.Ae.FindDescendantByAutomationId(ControlType.Tree, "leftTree"));
            this.RightTree = new Tree(app.Ae.FindDescendantByAutomationId(ControlType.Tree, "rightTree"));
        }

        public Tree LeftTree { get; set; }
        public Tree RightTree { get; set; }
    }
}