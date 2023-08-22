<script lang="ts">
    export let text: string = "Navigation link";
    export let href: string = "/";
    export let active: boolean = false;
    export let className: string = "";
</script>

<a {href}
   class="flex flex-row gap-x-4 items-center transition-colors duration-300 p-2 rounded-lg {className}">
    <slot/>
    <span
            class='{active ? "font-bold text-brand-500" : "text-dark-gray font-regular"} basic-animation group-hover:text-brand-500'>{text}</span>
</a>