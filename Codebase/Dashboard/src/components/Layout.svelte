<script>
    import SideBar from "./SideBar.svelte";
    import TopBar from "./TopBar.svelte";
</script>

<div class="flex flex-row h-full font-lato">
    <!--    sidebar    -->
    <SideBar/>
    <div class="flex flex-col">
        <div>
            <!--      top-bar      -->
            <TopBar/>
        </div>
        <div>
            <slot/>
        </div>
    </div>
</div>