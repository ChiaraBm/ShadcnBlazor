---
title: Carousel
description: A horizontally sliding carousel with per-slide CarouselItem children and Previous/Next icon-button navigation.
tags: [carousel, slider, slides, navigation, showcase]
---

# Carousel

A horizontally sliding carousel for showcasing a sequence of items — images, cards or any content — one at a time. Each `Carousel` wraps its slides in a `CarouselContent` that translates horizontally with a smooth 500ms ease-in-out transition, and every slide is a `CarouselItem` that registers itself with the carousel through a cascading value so navigation knows the total number of items. `CarouselPrevious` and `CarouselNext` are round icon buttons (rendered from the `Button` component with an `ArrowLeftIcon`/`ArrowRightIcon`) that sit outside the slide area at `-left-12` and `-right-12`, and are automatically disabled when the first or last slide is shown. Use it to display image galleries, featured content or onboarding steps with keyboard-free arrow navigation.

## Usage

```razor
@using ShadcnBlazor.Carousels

<Carousel>
    <CarouselPrevious />
    <CarouselContent>
        <CarouselItem>First slide</CarouselItem>
        <CarouselItem>Second slide</CarouselItem>
        <CarouselItem>Third slide</CarouselItem>
    </CarouselContent>
    <CarouselNext />
</Carousel>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | **(Carousel)** The inner content: place the `CarouselPrevious`, `CarouselContent` and `CarouselNext` components inside. The content is rendered inside a `<div role="region" aria-roledescription="carousel" data-slot="carousel">` wrapped in an `overflow-hidden` container. |
| `ChildContent` | `RenderFragment` | `null` | **(CarouselContent)** The horizontal track that holds the slides; it is a `flex` row with `transition-transform duration-500 ease-in-out` whose `transform: translateX(-{CurrentIndex * 100}%)` is driven by the parent `Carousel`'s current index. |
| `ChildContent` | `RenderFragment` | `null` | **(CarouselItem)** The content of a single slide, rendered in a `w-full shrink-0 pl-4` block. Each item increments the carousel's item count on initialization. |
| `Variant` | `ButtonVariant` | `ButtonVariant.Outline` | **(CarouselNext)** Button variant of the next-slide icon button. |
| `Size` | `ButtonSize` | `ButtonSize.Icon` | **(CarouselNext)** Button size of the next-slide icon button. |
| `Variant` | `ButtonVariant` | `ButtonVariant.Outline` | **(CarouselPrevious)** Button variant of the previous-slide icon button. |
| `Size` | `ButtonSize` | `ButtonSize.Icon` | **(CarouselPrevious)** Button size of the previous-slide icon button. |

All components inherit from `ShadcnBase`, so the base parameters (such as `ClassName` and `Props`) are also available. `ClassName` is appended to the built-in classes, and `Props` (a `Dictionary<string, object?>`) is merged into the rendered attributes. `Carousel` additionally exposes a public `CurrentIndex` (with private setter) and `NavigateAsync(int diff)` which moves the current index clamped between `0` and `NextItemIndex - 1`. `CarouselNext` navigates forward (`NavigateAsync(1)`) and is disabled at the last slide; `CarouselPrevious` navigates backward (`NavigateAsync(-1)`) and is disabled at the first slide.

## Examples

A basic showcase of image slides with the default round navigation buttons placed outside the track:

```razor
@using ShadcnBlazor.Carousels

<div class="relative mx-auto max-w-3xl px-16 py-12">
    <Carousel>
        <CarouselPrevious />
        <CarouselContent>
            <CarouselItem>
                <img src="https://placehold.co/600x400/0f172a/f8fafc?text=Slide+1" alt="Slide 1" />
            </CarouselItem>
            <CarouselItem>
                <img src="https://placehold.co/600x400/1e293b/f8fafc?text=Slide+2" alt="Slide 2" />
            </CarouselItem>
            <CarouselItem>
                <img src="https://placehold.co/600x400/334155/f8fafc?text=Slide+3" alt="Slide 3" />
            </CarouselItem>
        </CarouselContent>
        <CarouselNext />
    </Carousel>
</div>
```

Slides containing full card layouts (text plus icon), showing that any content can be placed inside a `CarouselItem`:

```razor
@using LucideBlazor
@using ShadcnBlazor.Carousels

<div class="relative mx-auto max-w-md px-16 py-12">
    <Carousel>
        <CarouselPrevious />
        <CarouselContent>
            <CarouselItem>
                <div class="flex h-56 flex-col justify-center rounded-lg bg-muted p-8 text-center">
                    <RocketIcon class="mx-auto mb-4 size-10" />
                    <h3 class="text-lg font-semibold">Ship faster</h3>
                    <p class="text-sm text-muted-foreground">Automated pipelines from commit to production.</p>
                </div>
            </CarouselItem>
            <CarouselItem>
                <div class="flex h-56 flex-col justify-center rounded-lg bg-muted p-8 text-center">
                    <ShieldCheckIcon class="mx-auto mb-4 size-10" />
                    <h3 class="text-lg font-semibold">Stay secure</h3>
                    <p class="text-sm text-muted-foreground">Zero-trust defaults and continuous scanning.</p>
                </div>
            </CarouselItem>
        </CarouselContent>
        <CarouselNext />
    </Carousel>
</div>
```

Custom navigation button appearance using the `Variant` and `Size` parameters of the arrow buttons:

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Carousels

<div class="relative mx-auto max-w-3xl px-16 py-12">
    <Carousel>
        <CarouselPrevious Variant="ButtonVariant.Default" Size="ButtonSize.Icon" />
        <CarouselContent>
            <CarouselItem>
                <div class="flex h-64 items-center justify-center rounded-lg bg-primary text-primary-foreground text-3xl font-bold">1</div>
            </CarouselItem>
            <CarouselItem>
                <div class="flex h-64 items-center justify-center rounded-lg bg-primary text-primary-foreground text-3xl font-bold">2</div>
            </CarouselItem>
        </CarouselContent>
        <CarouselNext Variant="ButtonVariant.Default" Size="ButtonSize.Icon" />
    </Carousel>
</div>
```

A minimal carousel without navigation buttons (e.g. when you drive the slides from code via `NavigateAsync`):

```razor
@using ShadcnBlazor.Carousels

@code {
    private Carousel? carousel;
}

<Carousel @ref="carousel">
    <CarouselContent>
        <CarouselItem>Intro</CarouselItem>
        <CarouselItem>Features</CarouselItem>
        <CarouselItem>Pricing</CarouselItem>
    </CarouselContent>
</Carousel>

<button class="btn" @onclick='async () => await carousel?.NavigateAsync(1)'>Next slide</button>
```
