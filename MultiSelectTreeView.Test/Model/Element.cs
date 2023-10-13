using System;
using System.Windows.Automation;
using MultiSelectTreeView.Test.Model.Helper;

namespace MultiSelectTreeView.Test.Model {
    class Element {
        public Element(AutomationElement automationElement) {
            this.Ae = automationElement;
        }

        internal AutomationElement Ae { get; private set; }

        public void Expand() {
            ExpandCollapsePattern expandPattern = this.Ae.GetPattern<ExpandCollapsePattern>();
            expandPattern.Expand();
        }

        public void Collapse() {
            ExpandCollapsePattern expandPattern = this.Ae.GetPattern<ExpandCollapsePattern>();
            expandPattern.Collapse();
        }

        public void Select() {
            SelectionItemPattern pattern = this.Ae.GetPattern<SelectionItemPattern>();
            pattern.Select();
        }

        public bool IsSelected {
            get {
                SelectionItemPattern pattern = this.Ae.GetPattern<SelectionItemPattern>();
                return pattern.Current.IsSelected;
            }
        }

        public bool IsExpanded {
            get {
                ExpandCollapsePattern pattern = this.Ae.GetPattern<ExpandCollapsePattern>();
                return pattern.Current.ExpandCollapseState == ExpandCollapseState.Expanded;
            }
        }

        public bool IsFocused {
            get {
                return Convert.ToBoolean(this.GetValue("node;IsFocused"));
            }
        }

        public bool IsSelectedPreview {
            get {
                return Convert.ToBoolean(this.GetValue("node;IsSelectedPreview"));
            }
        }

        public string GetValue(string id) {
            ValuePattern pattern = this.Ae.GetPattern<ValuePattern>();
            pattern.SetValue(id);
            return pattern.Current.Value;
        }
    }
}