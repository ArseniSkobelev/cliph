<script>
    import NavGroup from "./NavGroup.svelte";
    import NavLink from "./NavLink.svelte";
    import DashboardIcon from "./icons/DashboardIcon.svelte";
    import KeyIcon from "./icons/KeyIcon.svelte";
    import UsersIcon from "./icons/UsersIcon.svelte";
    import CogIcon from "./icons/CogIcon.svelte";

    import {page} from "$app/stores";
</script>

<div class="flex flex-col gap-y-16">
    <NavGroup groupName="Main">
        <NavLink text="Dashboard" href="/" active={$page.url.pathname === "/"}
                 className="group basic-animation hover:text-brand-500">
            <DashboardIcon height="32" width="32"
                           className='basic-animation group-hover:fill-brand-500 {$page.url.pathname
                           === "/" ? "fill-brand-500" : "fill-dark-gray"}'/>
        </NavLink>
        <NavLink text="Users" href="/users" active={$page.url.pathname === "/users"}
                 className="group basic-animation hover:text-brand-500">
            <UsersIcon height="32" width="32"
                       className='basic-animation group-hover:fill-brand-500 {$page.url.pathname
                           === "/users" ? "fill-brand-500" : "fill-dark-gray"}'/>
        </NavLink>
        <NavLink text="API keys" href="/keys" active={$page.url.pathname === "/keys"}
                 className="group basic-animation hover:text-brand-500">
            <KeyIcon height="32" width="32"
                     className='basic-animation group-hover:fill-brand-500 {$page.url.pathname === "/keys" ?
                     "fill-brand-500" : "fill-dark-gray"}'/>
        </NavLink>
    </NavGroup>
    <NavGroup groupName="Settings">
        <NavLink text="Settings" href="/settings" active={$page.url.pathname === "/settings"}
                 className="group basic-animation hover:text-brand-500">
            <CogIcon height="32" width="32"
                     className='basic-animation group-hover:fill-brand-500 {$page.url.pathname === "/settings" ?
                     "fill-brand-500" : "fill-dark-gray"}'/>
        </NavLink>
    </NavGroup>
</div>