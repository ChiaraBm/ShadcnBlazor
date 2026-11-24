---
title: Message
description: Chat message rows with optional avatar, header and footer, start/end alignment, and MessageGroup for stacked conversations.
tags: [message, chat, conversation, avatar, layout]
---

# Message

A chat message row for rendering individual messages in a conversation. Each `Message` renders a `div[data-slot="message"]` that combines an optional `MessageAvatar` on the left, a `MessageContent` that holds the actual content (usually one or more `Bubble` components), and optional `MessageHeader` / `MessageFooter` rows for labels such as names or "Read" status. The `Align` property (`MessageAlign.Start` or `MessageAlign.End`) flips the whole row so the avatar moves to the right side, and `MessageGroup` stacks multiple `Message` rows with tighter spacing. Use it to build full chat threads with avatars and footers, typically wrapping `Bubble` components in `MessageContent`.

## Usage

```razor
@using ShadcnBlazor.Messages

<Message>
    <MessageContent>
        Hello, world!
    </MessageContent>
</Message>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | **(Message)** The inner content of the row: typically a `MessageAvatar`, a `MessageContent`, and optionally a `MessageHeader` or `MessageFooter`. |
| `Align` | `MessageAlign` | `MessageAlign.Start` | **(Message)** Alignment of the row within its container. Values: `Start = 0`, `End = 1`. |
| `ChildContent` | `RenderFragment` | `null` | **(MessageAvatar)** The avatar content, typically an `Avatar` component. |
| `ChildContent` | `RenderFragment` | `null` | **(MessageContent)** The message content; place `Bubble` or `BubbleGroup` components here. |
| `ChildContent` | `RenderFragment` | `null` | **(MessageFooter)** The footer content, rendered below the message content, e.g. a read status or timestamp. |
| `ChildContent` | `RenderFragment` | `null` | **(MessageGroup)** The `Message` rows to stack in the group. |
| `ChildContent` | `RenderFragment` | `null` | **(MessageHeader)** The header content, rendered above the message content, e.g. the sender's name. |

All components inherit from `ShadcnBase`, so the base parameters (such as `ClassName` and `Props`) are also available. `ClassName` is appended to the component's default classes, and `Props` (a `Dictionary<string, object>?`) is merged into the rendered attributes.

## Examples

A full conversation with avatars, an end-aligned own message and a footer, adapted from the component showcase:

```razor
@using ShadcnBlazor.Avatars
@using ShadcnBlazor.Bubbles
@using ShadcnBlazor.Messages

<div class="flex w-full max-w-sm flex-col gap-6 py-12">
    <Message Align="MessageAlign.End">
        <MessageAvatar>
            <Avatar>
                <AvatarImage src="https://github.com/ChiaraBm.png" alt="me" />
                <AvatarFallback>ME</AvatarFallback>
            </Avatar>
        </MessageAvatar>
        <MessageContent>
            <Bubble>
                <BubbleContent>I'm updating the landing page copy now, give me a few minutes.</BubbleContent>
            </Bubble>
        </MessageContent>
    </Message>

    <Message>
        <MessageAvatar>
            <Avatar>
                <AvatarImage src="https://github.com/jesperrichert.png" alt="jesper" />
                <AvatarFallback>J</AvatarFallback>
            </Avatar>
        </MessageAvatar>
        <MessageContent>
            <Bubble Variant="BubbleVariant.Muted">
                <BubbleContent>The demo is ready in twenty. We're close.</BubbleContent>
            </Bubble>
        </MessageContent>
    </Message>

    <Message Align="MessageAlign.End">
        <MessageAvatar>
            <Avatar>
                <AvatarImage src="https://github.com/ChiaraBm.png" alt="me" />
                <AvatarFallback>ME</AvatarFallback>
            </Avatar>
        </MessageAvatar>
        <MessageContent>
            <Bubble>
                <BubbleContent>Done. The hero section looks sharp.</BubbleContent>
            </Bubble>
            <MessageFooter>Read</MessageFooter>
        </MessageContent>
    </Message>
</div>
```

Stacking several messages in a `MessageGroup`, with grouped bubbles and a reactions pill inside `MessageContent`:

```razor
@using ShadcnBlazor.Avatars
@using ShadcnBlazor.Bubbles
@using ShadcnBlazor.Messages

<MessageGroup>
    <Message>
        <MessageAvatar>
            <Avatar>
                <AvatarImage src="https://github.com/jesperrichert.png" alt="jesper" />
                <AvatarFallback>J</AvatarFallback>
            </Avatar>
        </MessageAvatar>
        <MessageContent>
            <BubbleGroup>
                <Bubble Variant="BubbleVariant.Muted">
                    <BubbleContent>
                        The preview is looking clean.
                    </BubbleContent>
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
</MessageGroup>
```

Using `MessageHeader` and `MessageFooter` around the message content:

```razor
@using ShadcnBlazor.Bubbles
@using ShadcnBlazor.Messages

<Message>
    <MessageContent>
        <MessageHeader>Jesper</MessageHeader>
        <Bubble Variant="BubbleVariant.Muted">
            <BubbleContent>The preview is looking clean.</BubbleContent>
        </Bubble>
        <MessageFooter>Just now</MessageFooter>
    </MessageContent>
</Message>
```
