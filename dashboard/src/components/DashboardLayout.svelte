<script>
    import Drawer from "./Drawer.svelte";
    import TopBar from "./TopBar.svelte";
</script>

<div class="flex flex-row h-full w-full">
    <Drawer />
    <div class="flex flex-col w-full">
        <TopBar />
        <slot />
    </div>
</div>