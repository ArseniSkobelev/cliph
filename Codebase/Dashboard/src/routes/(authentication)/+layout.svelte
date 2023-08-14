<script>
    import "../../app.css";
    import Logo from "../../components/icons/Logo.svelte";
</script>

<div class="flex flex-col items-center justify-center h-full gap-y-8 font-lato">
    <a href="/" class="flex flex-row gap-x-4 items-center max-w-fit">
        <Logo width="128" height="128"/>
        <span class="text-4xl font-semibold">Cliph</span>
    </a>
    <slot/>
</div>