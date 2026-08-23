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
        new("Form 1 Accordion", typeof(Form1Accordion)),
        new("Form 2 Accordion", typeof(Form2Accordion)),
        new("Full Featured Accordion", typeof(FullFeaturedAccordion)),
        new("Multi-level with Plus Trigger", typeof(MultiLevelAccordion)),
        new("Multi-level with Left Plus Trigger", typeof(MultiLevelLeftAccordion)),
        new("Standard Accordion", typeof(StandardAccordion)),
        new("Standard with Icon Trigger", typeof(StandardIconAccordion)),
        new("Standard with Left Chevron", typeof(StandardLeftChevronAccordion)),
        new("Standard with Plus Trigger", typeof(StandardPlusAccordion)),
        new("Standard with Icon and Plus Trigger", typeof(StandardIconPlusAccordion)),
        new("Standard with Left Plus Trigger", typeof(StandardLeftPlusAccordion)),
        new("Subtitle Accordion", typeof(SubtitleAccordion)),
        new("Tabs Accordion", typeof(TabsAccordion)),
        new("Error with Title", typeof(AlertError1)),
        new("Error with Title and Description", typeof(AlertError2)),
        new("Error with Title, Description, and Action", typeof(AlertError4)),
        new("Error with Everything", typeof(AlertError5)),
        new("Info with Title", typeof(AlertInfo1)),
        new("Info with Title and Description", typeof(AlertInfo2)),
        new("Info with Title, Description, and Action", typeof(AlertInfo4)),
        new("Info with Everything", typeof(AlertInfo5)),
        new("Standard with Title, Description, and Action", typeof(AlertStandard4)),
        new("Standard with Everything", typeof(AlertStandard5)),
        new("Success with Title and Action", typeof(AlertSuccess3)),
        new("Success with Title, Description, and Action", typeof(AlertSuccess4)),
        new("Warning with Title and Description", typeof(AlertWarning2)),
        new("Warning with Everything", typeof(AlertWarning5)),
        new("Breadcrumb with Icons", typeof(BreadcrumbHomeIcon3)),
        new("Breadcrumb with Border", typeof(BreadcrumbStandard4)),
        new("Document Actions Menu", typeof(ContextMenuFile1)),
        new("Context Menu with Icons and Shortcuts", typeof(ContextMenuStandard2)),
        new("Context Menu with Labels", typeof(ContextMenuStandard4)),
        new("Empty with Link Action", typeof(EmptyLinkAction)),
        new("Empty with Input Action", typeof(EmptyInputAction)),
        new("No Notifications", typeof(EmptyNoNotifications)),
        new("Empty with Search Action", typeof(EmptySearchAction)),
        new("No Search Results", typeof(EmptySearch1)),
        new("Empty with Multiple Paragraphs", typeof(EmptyMultipleParagraphs)),
        new("Choice Cards", typeof(ChoiceCards)),
        new("Time Input", typeof(TimeInput)),
        new("Password with Toggle", typeof(PasswordToggle)),
        new("Login Form", typeof(LoginForm)),
        new("AI Prompt Input", typeof(InputGroupAi1)),
        new("Simple AI Prompt", typeof(InputGroupAi2)),
        new("AI with Voice", typeof(InputGroupAi4)),
        new("Share Popover", typeof(SharePopover)),
        new("Feedback Popover", typeof(FeedbackPopover)),
        new("With Count", typeof(ProgressWithLabel4)),
        new("Multi-line Label", typeof(ProgressWithLabel5)),
        new("Radio Group with Custom Content", typeof(RadioGroupAdvanced2)),
        new("Notification List Sheet", typeof(NotificationListSheet)),
        new("Spinner in Item", typeof(SpinnerInItem)),
        new("Spinner in Empty State", typeof(SpinnerInEmptyState)),
        new("Tabs with Count Badges", typeof(TabsAdvanced1))
    ];
}

public class BlockEntry
{
    public string DisplayName { get; set; }
    
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public Type Component { get; set; }

    public BlockEntry(string displayName, Type component)
    {
        DisplayName = displayName;
        Component = component;
    }
}