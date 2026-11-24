---
title: Sidebar
description: Resizable, collapsible app navigation sidebar with header, grouped menu items, sub-items, badges, inset content and mobile off-canvas mode.
tags: [sidebar, navigation, layout, collapsible, menu, offcanvas]
---
# Sidebar

The Sidebar renders an application navigation rail that sits beside the main content. Wrap a page in a `SidebarProvider`, put a `Sidebar` inside it and fill it with `SidebarHeader`, `SidebarContent`, `SidebarFooter`, `SidebarMenu`/`SidebarMenuItem` items; a sibling `SidebarInset` holds the page content. It is collapsible (`OffCanvas`, `Icon` or fixed with `None`), positionable left or right, styled in `Sidebar`, `Floating` or `Inset` variants, and collapses into a `Sheet`-based off-canvas drawer automatically on small screens. Toggle it with `SidebarTrigger` or `SidebarRail`, react to state changes with the provider's `OnOpenChange` callback, and use `Slot` on menu buttons to render links instead of plain buttons.

## Usage

```razor
@using ShadcnBlazor.Sidebars

<SidebarProvider DefaultOpen="true">
    <Sidebar>
        <SidebarHeader>
            <SidebarMenu>
                <SidebarMenuItem>
                    <SidebarMenuButton>
                        <span>ShadcnBlazor</span>
                    </SidebarMenuButton>
                </SidebarMenuItem>
            </SidebarMenu>
        </SidebarHeader>

        <SidebarContent>
            <SidebarGroup>
                <SidebarGroupContent>
                    <SidebarMenu>
                        <SidebarMenuItem>
                            <SidebarMenuButton IsActive="true">
                                <span>Home</span>
                            </SidebarMenuButton>
                        </SidebarMenuItem>
                    </SidebarMenu>
                </SidebarGroupContent>
            </SidebarGroup>
        </SidebarContent>
    </Sidebar>

    <SidebarInset>
        <div class="p-4">
            <SidebarTrigger />
            Page content
        </div>
    </SidebarInset>
</SidebarProvider>
```

## Parameters

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| ChildContent | RenderFragment | — | The rendered content of the component. Declared by `Sidebar`, `SidebarProvider`, `SidebarHeader`, `SidebarContent`, `SidebarFooter`, `SidebarGroup`, `SidebarGroupAction`, `SidebarGroupContent`, `SidebarGroupLabel`, `SidebarInset`, `SidebarMenu`, `SidebarMenuItem`, `SidebarMenuAction`, `SidebarMenuBadge`, `SidebarMenuButton`, `SidebarMenuSub`, `SidebarMenuSubButton`, `SidebarMenuSubItem`, `SidebarRail` and `SidebarSeparator`. |
| Side | SidebarSide | `SidebarSide.Left` | Which edge of the viewport the sidebar is docked to. `SidebarSide.Left` (0) or `SidebarSide.Right` (1) (`Sidebar`). |
| Variant | SidebarVariant | `SidebarVariant.Sidebar` | The sidebar look. `SidebarVariant.Sidebar` (0) is flush, `SidebarVariant.Floating` (1) adds padding, rounded corners and a shadow, `SidebarVariant.Inset` (2) is flush with a border (`Sidebar`). |
| Collapsible | SidebarCollapsible | `SidebarCollapsible.OffCanvas` | How the sidebar collapses. `SidebarCollapsible.OffCanvas` (0) slides off the edge and is a `Sheet` on mobile, `SidebarCollapsible.Icon` (1) shrinks to icons only, `SidebarCollapsible.None` (2) is a fixed always-open bar that ignores the trigger and rail (`Sidebar`). |
| DefaultOpen | bool | `true` | Whether the sidebar is open on first render (`SidebarProvider`). |
| OnOpenChange | EventCallback\<bool\> | — | Invoked whenever the open state changes: with the new open state on desktop, or with the mobile sheet open/closed state (`SidebarProvider`). |
| Slot | RenderFragment\<Dictionary\<string, object\>\>\>? | null | A render fragment that receives the button attributes as a `context` dictionary, so the default button can be swapped for e.g. an `<a href>` (`SidebarGroupAction`, `SidebarGroupLabel`, `SidebarMenuAction`, `SidebarMenuButton`, `SidebarMenuSubButton`). |
| ShowOnHover | bool | `false` | Makes a menu action only visible while its item is hovered or open (`SidebarMenuAction`). |
| Variant | SidebarMenuButtonVariant | `SidebarMenuButtonVariant.Default` | `SidebarMenuButtonVariant.Default` (0) or `SidebarMenuButtonVariant.Outline` (1), which draws a background and border shadow (`SidebarMenuButton`). |
| Size | SidebarMenuButtonSize | `SidebarMenuButtonSize.Default` | `SidebarMenuButtonSize.Default` (0), `SidebarMenuButtonSize.Sm` (1) or `SidebarMenuButtonSize.Lg` (2) (`SidebarMenuButton`). |
| IsActive | bool | `false` | Highlights the item in the active accent style (`SidebarMenuButton`, `SidebarMenuSubButton`). |
| ShowIcon | bool | `false` | Also renders a square icon placeholder before the text placeholder (`SidebarMenuSkeleton`). |
| Size | SidebarMenuSubButtonSize | `SidebarMenuSubButtonSize.Md` | `SidebarMenuSubButtonSize.Sm` (0) or `SidebarMenuSubButtonSize.Md` (1) (`SidebarMenuSubButton`). |
| BindEvent | string | `"onchange"` | The DOM event that the text input binds to (`SidebarInput`). |
| ClassName | string | — | Extra Tailwind classes merged into the root element. Inherited from `ShadcnBase` by all components; declared directly by `SidebarInput` (a form input, not `ShadcnBase`). |
| Props | Dictionary\<string, object\>? | — | Catch-all for unmatched attributes, applied to the root element. Inherited from `ShadcnBase` by all components except `SidebarInput`. |

`SidebarInput` additionally exposes the standard `Microsoft.AspNetCore.Components.Forms.InputBase<string>` properties (`Value`, `ValueChanged`, `EditContext`, `Expression`, `FieldIdentifier`, `CssClass`) for data binding and validation. `SidebarRail` and `SidebarTrigger` receive the `SidebarProvider` through a cascading parameter rather than a `[Parameter]`, and `ToggleSidebarAsync` on the provider is invoked by both.

## Examples

### Collapsible icon sidebar with an in-content trigger

The classic app shell: the sidebar collapses to a narrow icon rail when toggled, and the trigger button lives in the `SidebarInset` header. This is the structure used by the ShadcnBlazor demo site's `MainLayout.razor`.

```razor
@using LucideBlazor
@using ShadcnBlazor.Sidebars

<SidebarProvider DefaultOpen="true">
    <Sidebar Variant="SidebarVariant.Inset" Collapsible="SidebarCollapsible.Icon">
        <SidebarHeader>
            <SidebarMenu>
                <SidebarMenuItem>
                    <SidebarMenuButton>
                        <a href="#" class="flex flex-row items-center">
                            <span class="ms-2.5 text-lg font-semibold">ShadcnBlazor</span>
                        </a>
                    </SidebarMenuButton>
                </SidebarMenuItem>
            </SidebarMenu>
        </SidebarHeader>

        <SidebarContent>
            <SidebarGroup>
                <SidebarGroupContent>
                    <SidebarMenu>
                        <SidebarMenuItem>
                            <SidebarMenuButton IsActive="true">
                                <ShapesIcon />
                                <span>Components</span>
                            </SidebarMenuButton>
                        </SidebarMenuItem>

                        <SidebarMenuItem>
                            <SidebarMenuButton>
                                <WrenchIcon />
                                <span>Installation</span>
                            </SidebarMenuButton>
                        </SidebarMenuItem>

                        <SidebarMenuItem>
                            <SidebarMenuButton>
                                <SparklesIcon />
                                <span>Extras</span>
                            </SidebarMenuButton>
                        </SidebarMenuItem>

                        <SidebarMenuItem>
                            <SidebarMenuButton>
                                <BlocksIcon />
                                <span>Blocks</span>
                            </SidebarMenuButton>
                        </SidebarMenuItem>
                    </SidebarMenu>
                </SidebarGroupContent>
            </SidebarGroup>
        </SidebarContent>

        <SidebarFooter>
            <SidebarSeparator />
            <SidebarMenu>
                <SidebarMenuItem>
                    <SidebarMenuButton Size="SidebarMenuButtonSize.Lg">
                        <span>Sign out</span>
                    </SidebarMenuButton>
                </SidebarMenuItem>
            </SidebarMenu>
        </SidebarFooter>
    </Sidebar>

    <SidebarInset>
        <header
            class="flex h-12 shrink-0 items-center gap-2 border-b transition-[width,height] ease-linear group-has-data-[collapsible=icon]/sidebar-wrapper:h-12"
            style="--header-height: calc(var(--spacing) * 12)">
            <div class="flex w-full items-center gap-1 px-4 lg:gap-2 lg:px-6">
                <SidebarTrigger ClassName="-ml-1" />
            </div>
        </header>

        <div class="mx-8 my-8 max-w-full">
            @* Page content *@
        </div>
    </SidebarInset>
</SidebarProvider>
```

### Off-canvas sidebar with open-state change handling

By default the sidebar slides off the edge (`OffCanvas`) and becomes a mobile sheet. Use the `DefaultOpen` and `OnOpenChange` parameters on `SidebarProvider` to control and observe the state.

```razor
@using ShadcnBlazor.Sidebars

<SidebarProvider DefaultOpen="false" OnOpenChange="OnSidebarToggled">
    <Sidebar>
        <SidebarHeader>
            <SidebarMenu>
                <SidebarMenuItem>
                    <SidebarMenuButton>
                        <span>My app</span>
                    </SidebarMenuButton>
                </SidebarMenuItem>
            </SidebarMenu>
        </SidebarHeader>

        <SidebarContent>
            <SidebarGroup>
                <SidebarGroupContent>
                    <SidebarMenu>
                        <SidebarMenuItem>
                            <SidebarMenuButton Variant="SidebarMenuButtonVariant.Outline">
                                <ShapesIcon />
                                <span>Components</span>
                            </SidebarMenuButton>
                        </SidebarMenuItem>
                    </SidebarMenu>
                </SidebarGroupContent>
            </SidebarGroup>
        </SidebarContent>
    </Sidebar>

    <SidebarInset>
        <div class="p-4">
            <SidebarTrigger />
            <p>Open state: @IsSidebarOpen</p>
        </div>
    </SidebarInset>
</SidebarProvider>

@code
{
    private bool IsSidebarOpen = false;

    private async Task OnSidebarToggled(bool open)
    {
        IsSidebarOpen = open;
        await InvokeAsync(StateHasChanged);
    }
}
```

### Menu items with sub-items, badges, group actions and a user dropdown

`SidebarMenuSub` holds nested items. `SidebarMenuBadge` and `SidebarMenuAction` (with `ShowOnHover`) decorate the main item, and `Slot` swaps the button for a link — used here to wrap a `DropdownMenu` inside `SidebarMenuButton`, mirroring the `NavUser` component from the demo site.

```razor
@using LucideBlazor
@using ShadcnBlazor.Avatars
@using ShadcnBlazor.Dropdowns
@using ShadcnBlazor.Sidebars

<SidebarProvider DefaultOpen="true">
    <Sidebar>
        <SidebarContent>
            <SidebarGroup>
                <SidebarGroupLabel>Menu</SidebarGroupLabel>
                <SidebarGroupAction>
                    <ShapesIcon />
                </SidebarGroupAction>

                <SidebarGroupContent>
                    <SidebarMenu>
                        <SidebarMenuItem>
                            <SidebarMenuButton IsActive="true">
                                <WrenchIcon />
                                <span>Dashboard</span>
                                <SidebarMenuBadge>42</SidebarMenuBadge>
                                <SidebarMenuAction ShowOnHover>
                                    <ChevronsUpDownIcon />
                                </SidebarMenuAction>
                            </SidebarMenuButton>

                            <SidebarMenuSub>
                                <SidebarMenuSubItem>
                                    <SidebarMenuSubButton IsActive="true" Size="SidebarMenuSubButtonSize.Sm">
                                        <span>Sub page A</span>
                                    </SidebarMenuSubButton>
                                </SidebarMenuSubItem>
                                <SidebarMenuSubItem>
                                    <SidebarMenuSubButton Size="SidebarMenuSubButtonSize.Md">
                                        <span>Sub page B</span>
                                    </SidebarMenuSubButton>
                                </SidebarMenuSubItem>
                            </SidebarMenuSub>
                        </SidebarMenuItem>
                    </SidebarMenu>
                </SidebarGroupContent>
            </SidebarGroup>

            <SidebarGroup>
                <SidebarGroupContent>
                    <SidebarMenu>
                        <SidebarMenuItem>
                            <DropdownMenu>
                                <DropdownMenuTrigger>
                                    <Slot>
                                        <SidebarMenuButton
                                            Size="SidebarMenuButtonSize.Lg"
                                            ClassName="data-[state=open]:bg-sidebar-accent data-[state=open]:text-sidebar-accent-foreground"
                                            @attributes="context">
                                            <Avatar className="h-8 w-8 rounded-lg">
                                                <AvatarImage src="https://github.com/ghost.png" alt="Ghost"/>
                                                <AvatarFallback className="rounded-lg">GH</AvatarFallback>
                                            </Avatar>
                                            <div class="grid flex-1 text-left text-sm leading-tight">
                                                <span class="truncate font-medium">Ghost</span>
                                                <span class="truncate text-xs">ghost@github.com</span>
                                            </div>
                                            <ChevronsUpDownIcon ClassName="ml-auto size-4" />
                                        </SidebarMenuButton>
                                    </Slot>
                                </DropdownMenuTrigger>
                                <DropdownMenuContent ClassName="w-56 rounded-lg">
                                    <DropdownMenuItem>
                                        <SparklesIcon />
                                        Upgrade to Pro
                                    </DropdownMenuItem>
                                    <DropdownMenuSeparator/>
                                    <DropdownMenuItem>
                                        <BlocksIcon />
                                        Sign out
                                    </DropdownMenuItem>
                                </DropdownMenuContent>
                            </DropdownMenu>
                        </SidebarMenuItem>
                    </SidebarMenu>
                </SidebarGroupContent>
            </SidebarGroup>
        </SidebarContent>
    </Sidebar>

    <SidebarInset>
        <div class="p-4">
            <SidebarTrigger />
        </div>
    </SidebarInset>
</SidebarProvider>
```

### Fixed right-side sidebar with loading skeletons

With `SidebarCollapsible.None` the bar is always open and ignores the trigger. `SidebarMenuSkeleton` provides a loading placeholder whose icon square and text width (a random 50–90 %) mimic real items.

```razor
@using ShadcnBlazor.Sidebars

<SidebarProvider DefaultOpen="true">
    <Sidebar Collapsible="SidebarCollapsible.None" Side="SidebarSide.Right">
        <SidebarContent>
            <SidebarGroup>
                <SidebarGroupContent>
                    <SidebarMenu>
                        @for (int i = 0; i < 6; i++)
                        {
                            <SidebarMenuItem @key="i">
                                <SidebarMenuSkeleton ShowIcon="true" />
                            </SidebarMenuItem>
                        }
                    </SidebarMenu>
                </SidebarGroupContent>
            </SidebarGroup>
        </SidebarContent>
    </Sidebar>

    <SidebarInset>
        <div class="p-4">Page content</div>
    </SidebarInset>
</SidebarProvider>
```
