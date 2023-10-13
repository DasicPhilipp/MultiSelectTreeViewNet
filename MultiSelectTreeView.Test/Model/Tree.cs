using MultiSelectTreeView.Test.Model.Helper;
using System.Windows.Automation;

namespace MultiSelectTreeView.Test.Model {
    class Tree {
        AutomationElement treeAutomationElement;
        Element element1;
        Element element11;
        Element element12;
        Element element13;
        Element element14;
        Element element15;

        public Tree(AutomationElement treeAutomationElement) {
            this.treeAutomationElement = treeAutomationElement;
        }

        public Element Element1 { get { return this.element1 != null ? this.element1 : this.element1 = new Element(this.treeAutomationElement.FindFirstDescendant(ControlType.TreeItem)); } }
        public Element Element11 { get { return this.element11 != null ? this.element11 : this.element11 = new Element(this.Element1.Ae.FindDescendantByName(ControlType.TreeItem, "element11")); } }
        public Element Element12 { get { return this.element12 != null ? this.element12 : this.element12 = new Element(this.Element1.Ae.FindDescendantByName(ControlType.TreeItem, "element12")); } }
        public Element Element13 { get { return this.element13 != null ? this.element13 : this.element13 = new Element(this.Element1.Ae.FindDescendantByName(ControlType.TreeItem, "element13")); } }
        public Element Element14 { get { return this.element14 != null ? this.element14 : this.element14 = new Element(this.Element1.Ae.FindDescendantByName(ControlType.TreeItem, "element14")); } }
        public Element Element15 { get { return this.element15 != null ? this.element15 : this.element15 = new Element(this.Element1.Ae.FindDescendantByName(ControlType.TreeItem, "element15")); } }
    }
}