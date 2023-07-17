<script>
    import NavGroup from "./NavGroup.svelte";
    import NavLink from "./NavLink.svelte";
    import DashboardIcon from "./icons/DashboardIcon.svelte";
    import KeyIcon from "./icons/KeyIcon.svelte";
    import UsersIcon from "./icons/UsersIcon.svelte";
</script>

<div class="flex flex-col gap-y-16">
    <NavGroup groupName="Main">
        <NavLink text="Dashboard" active={true}>
            <DashboardIcon height="32" width="32" className="fill-brand-500"/>
        </NavLink>
        <NavLink text="Users" active={false}>
            <UsersIcon height="32" width="32"/>
        </NavLink>
        <NavLink text="API keys" active={false}>
            <KeyIcon height="32" width="32" className="fill-dark-gray"/>
        </NavLink>
    </NavGroup>
    <NavGroup groupName="Settings">
        <h2>Test</h2>
        <h2>Test2</h2>
    </NavGroup>
</div>