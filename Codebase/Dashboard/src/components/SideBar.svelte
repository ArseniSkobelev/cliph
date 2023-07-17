<script>
    import Logo from "./icons/Logo.svelte";
    import Nav from "./Nav.svelte";
</script>

<div class="bg-light-gray w-80 h-full py-14 px-8">
    <div>
        <a href="/" class="gap-x-4 flex flex-row items-center max-w-fit mb-16">
            <Logo width="64" height="64"/>
            <span class="text-3xl font-semibold">Cliph</span>
        </a>
    </div>
    <Nav/>
</div>