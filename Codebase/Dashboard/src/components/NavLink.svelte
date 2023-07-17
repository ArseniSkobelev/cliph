<script lang="ts">
    export let text: string = "Navigation link";
    export let href: string = "/";
    export let active: boolean = false;
</script>

<a {href} class="flex flex-row gap-x-4 items-center">
    <slot/>
    <span class='{active ? "font-bold text-brand-500" : "text-dark-gray font-regular"}'>{text}</span>
</a>