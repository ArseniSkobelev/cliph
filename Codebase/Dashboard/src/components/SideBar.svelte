<script>
    import Logo from "./icons/Logo.svelte";
    import Nav from "./Nav.svelte";
    import LogoutIcon from "./icons/LogoutIcon.svelte";
    import InformationIcon from "./icons/InformationIcon.svelte";
</script>

<div class="bg-light-gray w-80 h-full py-14 px-8 flex flex-col">
    <div>
        <a href="/" class="gap-x-4 flex flex-row items-center max-w-fit mb-16">
            <Logo width="64" height="64"/>
            <span class="text-3xl font-semibold">Cliph</span>
        </a>
    </div>
    <Nav/>
    <div class="flex flex-col gap-y-4 mt-auto">
        <a class="flex flex-row gap-x-4 text-dark-gray items-center group" href="/logout">
            <LogoutIcon height="32" width="32"
                        className="fill-dark-gray group-hover:fill-brand-300 basic-animation"/>
            <span class="group-hover:text-brand-300 basic-animation">Logout</span>
        </a>
        <button class="flex flex-row gap-x-4 text-dark-gray items-center group">
            <InformationIcon height="32" width="32"
                             dotClass="fill-dark-gray group-hover:fill-brand-300 basic-animation"
                             lineClass="stroke-dark-gray group-hover:stroke-brand-300 basic-animation"
                             circleClass="stroke-dark-gray group-hover:stroke-brand-300 basic-animation"/>
            <span class="group-hover:text-brand-300 basic-animation">Help and information</span>
        </button>
    </div>
</div>