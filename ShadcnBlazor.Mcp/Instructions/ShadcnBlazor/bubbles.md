---
title: Bubble
description: Chat message bubbles with variants and start/end alignment, groupable in a BubbleGroup with optional reaction pills.
tags: [bubble, chat, message, conversation, reactions]
---

# Bubble

A chat-style message bubble for rendering individual messages in a conversation. Each `Bubble` renders a `div[data-slot="bubble"]` that hosts a `BubbleContent` (the styled message body) and, optionally, a floating `BubbleReactions` pill positioned on a chosen side and alignment. Bubbles support seven visual variants (`Default`, `Secondary`, `Muted`, `Tinted`, `Outline`, `Ghost`, `Destructive`) and start/end alignment, and consecutive messages can be grouped together in a `BubbleGroup` to get tighter spacing. Use it on its own for a single message or inside the `Message` component for full chat rows with avatars and footers.

## Usage

```razor
@using ShadcnBlazor.Bubbles

<Bubble>
    <BubbleContent>Hello, world!</BubbleContent>
</Bubble>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | **(Bubble)** The inner content of the bubble: place a `BubbleContent` and optionally a `BubbleReactions` inside. |
| `Variant` | `BubbleVariant` | `BubbleVariant.Default` | **(Bubble)** Visual style of the bubble. Values: `Default = 0`, `Secondary = 1`, `Muted = 2`, `Tinted = 3`, `Outline = 4`, `Ghost = 5`, `Destructive = 6`. |
| `Align` | `BubbleAlign` | `BubbleAlign.Start` | **(Bubble)** Alignment of the bubble within its container. Values: `Start = 0`, `End = 1`. |
| `ChildContent` | `RenderFragment` | `null` | **(BubbleContent)** The message text or content rendered inside the styled content box. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | **(BubbleContent)** When set, this template is invoked with the computed attributes instead of rendering the default `<div>`; use it to render the content in a different element. |
| `ChildContent` | `RenderFragment` | `null` | **(BubbleGroup)** The bubbles to render in the group. |
| `ChildContent` | `RenderFragment` | `null` | **(BubbleReactions)** The reaction content, typically a set of `<span>` emojis or icons. |
| `Align` | `BubbleAlign` | `BubbleAlign.End` | **(BubbleReactions)** Horizontal alignment of the reactions pill. Values: `Start = 0`, `End = 1`. |
| `Side` | `BubbleReactionsSide` | `BubbleReactionsSide.Bottom` | **(BubbleReactions)** Vertical side the reactions pill floats on. Values: `Bottom = 0`, `Top = 1`. |

All components inherit from `ShadcnBase`, so the base parameters (such as `ClassName` and `Props`) are also available. `ClassName` is appended to the variant classes, and `Props` (a `Dictionary<string, object?>`) is merged into the rendered attributes (for `BubbleContent` it is merged into the content box's attributes).

## Examples

A grouped conversation with different variants and a reactions pill:

```razor
@using ShadcnBlazor.Bubbles

<div class="flex w-full max-w-sm flex-col gap-8 py-12">
    <Bubble Align="BubbleAlign.End">
        <BubbleContent>Morning! Ready to poke at the new chat components?</BubbleContent>
    </Bubble>
    <BubbleGroup>
        <Bubble Variant="BubbleVariant.Muted">
            <BubbleContent>Ready. The Message port just landed.</BubbleContent>
        </Bubble>
        <Bubble Variant="BubbleVariant.Muted">
            <BubbleContent>
                Bubbles, markers and shimmer
            </BubbleContent>
            <BubbleReactions role="img" aria-label="Reaction: thumbs up">
                <span>👍</span>
            </BubbleReactions>
        </Bubble>
    </BubbleGroup>
    <Bubble Variant="BubbleVariant.Muted">
        <BubbleContent>
            Save me a seat next time. I'll bring the coffee.
        </BubbleContent>
        <BubbleReactions role="img" aria-label="Reactions: thumbs up, fire, eyes, and 2 more">
            <span>👍</span>
            <span>🔥</span>
            <span>👀</span>
            <span>+2</span>
        </BubbleReactions>
    </Bubble>
</div>
```

Reactions pill placed on the top side and aligned to the start of the bubble:

```razor
@using ShadcnBlazor.Bubbles

<Bubble Variant="BubbleVariant.Muted">
    <BubbleContent>What a great idea!</BubbleContent>
    <BubbleReactions Align="BubbleAlign.Start" Side="BubbleReactionsSide.Top">
        <span>🎉</span>
        <span>❤️</span>
    </BubbleReactions>
</Bubble>
```

Bubbles nested inside the `Message` component, combined with `Avatar` for a full chat row:

```razor
@using ShadcnBlazor.Avatars
@using ShadcnBlazor.Bubbles
@using ShadcnBlazor.Messages

<div class="flex w-full max-w-sm flex-col gap-6 py-12">
    <Message>
        <MessageAvatar>
            <Avatar>
                <AvatarImage src="https://github.com/ChiaraBm.png" alt="me" />
                <AvatarFallback>ME</AvatarFallback>
            </Avatar>
        </MessageAvatar>
        <MessageContent>
            <BubbleGroup>
                <Bubble Variant="BubbleVariant.Muted">
                    <BubbleContent>The preview is looking clean.</BubbleContent>
                </Bubble>
                <Bubble Variant="BubbleVariant.Muted">
                    <BubbleContent>Great, I'll share the demo link tonight.</BubbleContent>
                    <BubbleReactions aria-label="Reactions: thumbs up">
                        <span>👍</span>
                    </BubbleReactions>
                </Bubble>
            </BubbleGroup>
        </MessageContent>
    </Message>
</div>
```

Showing every available variant:

```razor
@using ShadcnBlazor.Bubbles

<div class="flex w-full max-w-sm flex-col gap-3">
    <Bubble Variant="BubbleVariant.Default"><BubbleContent>Default</BubbleContent></Bubble>
    <Bubble Variant="BubbleVariant.Secondary"><BubbleContent>Secondary</BubbleContent></Bubble>
    <Bubble Variant="BubbleVariant.Muted"><BubbleContent>Muted</BubbleContent></Bubble>
    <Bubble Variant="BubbleVariant.Tinted"><BubbleContent>Tinted</BubbleContent></Bubble>
    <Bubble Variant="BubbleVariant.Outline"><BubbleContent>Outline</BubbleContent></Bubble>
    <Bubble Variant="BubbleVariant.Ghost"><BubbleContent>Ghost</BubbleContent></Bubble>
    <Bubble Variant="BubbleVariant.Destructive"><BubbleContent>Destructive</BubbleContent></Bubble>
</div>
```
