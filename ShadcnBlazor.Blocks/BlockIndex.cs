using System.Diagnostics.CodeAnalysis;
using ShadcnBlazor.Blocks.Accordions;
using ShadcnBlazor.Blocks.Alerts;
using ShadcnBlazor.Blocks.Breadcrumbs;
using ShadcnBlazor.Blocks.ContextMenus;
using ShadcnBlazor.Blocks.Emptys;
using ShadcnBlazor.Blocks.Fields;
using ShadcnBlazor.Blocks.Forms;
using ShadcnBlazor.Blocks.InputGroups;
using ShadcnBlazor.Blocks.Items;
using ShadcnBlazor.Blocks.Popovers;
using ShadcnBlazor.Blocks.Progress;
using ShadcnBlazor.Blocks.RadioGroups;
using ShadcnBlazor.Blocks.Sheets;
using ShadcnBlazor.Blocks.Spinners;
using ShadcnBlazor.Blocks.Tabs;

namespace ShadcnBlazor.Blocks;

public class BlockIndex
{
    public static List<BlockEntry> Entries =
    [
        new("Form 1 Accordion", "Accordions", typeof(Form1Accordion)),
        new("Form 2 Accordion", "Accordions", typeof(Form2Accordion)),
        new("Full Featured Accordion", "Accordions", typeof(FullFeaturedAccordion)),
        new("Multi-level with Plus Trigger", "Accordions", typeof(MultiLevelAccordion)),
        new("Multi-level with Left Plus Trigger", "Accordions", typeof(MultiLevelLeftAccordion)),
        new("Standard Accordion", "Accordions", typeof(StandardAccordion)),
        new("Standard with Icon Trigger", "Accordions", typeof(StandardIconAccordion)),
        new("Standard with Left Chevron", "Accordions", typeof(StandardLeftChevronAccordion)),
        new("Standard with Plus Trigger", "Accordions", typeof(StandardPlusAccordion)),
        new("Standard with Icon and Plus Trigger", "Accordions", typeof(StandardIconPlusAccordion)),
        new("Standard with Left Plus Trigger", "Accordions", typeof(StandardLeftPlusAccordion)),
        new("Subtitle Accordion", "Accordions", typeof(SubtitleAccordion)),
        new("Tabs Accordion", "Accordions", typeof(TabsAccordion)),
        new("Error with Title", "Alerts", typeof(AlertError1)),
        new("Error with Title and Description", "Alerts", typeof(AlertError2)),
        new("Error with Title, Description, and Action", "Alerts", typeof(AlertError4)),
        new("Error with Everything", "Alerts", typeof(AlertError5)),
        new("Info with Title", "Alerts", typeof(AlertInfo1)),
        new("Info with Title and Description", "Alerts", typeof(AlertInfo2)),
        new("Info with Title, Description, and Action", "Alerts", typeof(AlertInfo4)),
        new("Info with Everything", "Alerts", typeof(AlertInfo5)),
        new("Standard with Title, Description, and Action", "Alerts", typeof(AlertStandard4)),
        new("Standard with Everything", "Alerts", typeof(AlertStandard5)),
        new("Success with Title and Action", "Alerts", typeof(AlertSuccess3)),
        new("Success with Title, Description, and Action", "Alerts", typeof(AlertSuccess4)),
        new("Warning with Title and Description", "Alerts", typeof(AlertWarning2)),
        new("Warning with Everything", "Alerts", typeof(AlertWarning5)),
        new("Breadcrumb with Icons", "Breadcrumbs", typeof(BreadcrumbHomeIcon3)),
        new("Breadcrumb with Border", "Breadcrumbs", typeof(BreadcrumbStandard4)),
        new("Document Actions Menu", "Context Menus", typeof(ContextMenuFile1)),
        new("Context Menu with Icons and Shortcuts", "ContextMenus", typeof(ContextMenuStandard2)),
        new("Context Menu with Labels", "ContextMenus", typeof(ContextMenuStandard4)),
        new("Empty with Link Action", "Emptys", typeof(EmptyLinkAction)),
        new("Empty with Input Action", "Emptys", typeof(EmptyInputAction)),
        new("No Notifications", "Emptys", typeof(EmptyNoNotifications)),
        new("Empty with Search Action", "Emptys", typeof(EmptySearchAction)),
        new("No Search Results", "Emptys", typeof(EmptySearch1)),
        new("Empty with Multiple Paragraphs", "Emptys", typeof(EmptyMultipleParagraphs)),
        new("Choice Cards", "Fields", typeof(ChoiceCards)),
        new("Time Input", "Fields", typeof(TimeInput)),
        new("Password with Toggle", "Fields", typeof(PasswordToggle)),
        new("Login Form", "Forms", typeof(LoginForm)),
        new("AI Prompt Input", "InputGroups", typeof(InputGroupAi1)),
        new("Simple AI Prompt", "InputGroups", typeof(InputGroupAi2)),
        new("AI with Voice", "InputGroups", typeof(InputGroupAi4)),
        new("Share Popover", "Popovers", typeof(SharePopover)),
        new("Feedback Popover", "Popovers", typeof(FeedbackPopover)),
        new("With Count", "Progress", typeof(ProgressWithLabel4)),
        new("Multi-line Label", "Progress", typeof(ProgressWithLabel5)),
        new("Radio Group with Custom Content", "RadioGroups", typeof(RadioGroupAdvanced2)),
        new("Notification List Sheet", "Sheets", typeof(NotificationListSheet)),
        new("Spinner in Item", "Items", typeof(SpinnerInItem)),
        new("Spinner in Empty State", "Spinners", typeof(SpinnerInEmptyState)),
        new("Tabs with Count Badges", "Tabs", typeof(TabsAdvanced1))
    ];
}

public class BlockEntry
{
    public string DisplayName { get; set; }
    public string Group { get; set; }
    
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public Type Component { get; set; }

    public BlockEntry(string displayName, string group, Type component)
    {
        DisplayName = displayName;
        Group = group;
        Component = component;
    }
}
