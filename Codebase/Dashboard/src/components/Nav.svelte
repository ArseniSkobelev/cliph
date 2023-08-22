<script>
    import NavGroup from "./NavGroup.svelte";
    import NavLink from "./NavLink.svelte";
    import DashboardIcon from "./icons/DashboardIcon.svelte";
    import KeyIcon from "./icons/KeyIcon.svelte";
    import UsersIcon from "./icons/UsersIcon.svelte";
    import CogIcon from "./icons/CogIcon.svelte";

    import {page} from "$app/stores";
</script>

<div class="flex flex-col gap-y-8 sm:gap-y-16">
    <NavGroup groupName="Main">
        <NavLink text="Dashboard" href="/dashboard" active={$page.url.pathname === "/dashboard"}
                 className="group basic-animation hover:text-brand-500">
            <DashboardIcon height="32" width="32"
                           className='basic-animation group-hover:fill-brand-500 {$page.url.pathname
                           === "/dashboard" ? "fill-brand-500" : "fill-dark-gray"}'/>
        </NavLink>
        <NavLink text="Users" href="/dashboard/users" active={$page.url.pathname === "/dashboard/users"}
                 className="group basic-animation hover:text-brand-500">
            <UsersIcon height="32" width="32"
                       className='basic-animation group-hover:fill-brand-500 {$page.url.pathname
                           === "/dashboard/users" ? "fill-brand-500" : "fill-dark-gray"}'/>
        </NavLink>
        <NavLink text="API keys" href="/dashboard/keys" active={$page.url.pathname === "/dashboard/keys"}
                 className="group basic-animation hover:text-brand-500">
            <KeyIcon height="32" width="32"
                     className='basic-animation group-hover:fill-brand-500 {$page.url.pathname === "/dashboard/keys" ?
                     "fill-brand-500" : "fill-dark-gray"}'/>
        </NavLink>
    </NavGroup>
    <NavGroup groupName="Settings">
        <NavLink text="Settings" href="/dashboard/settings" active={$page.url.pathname === "/dashboard/settings"}
                 className="group basic-animation hover:text-brand-500">
            <CogIcon height="32" width="32"
                     className='basic-animation group-hover:fill-brand-500 {$page.url.pathname ===
                     "/dashboard/settings" ?
                     "fill-brand-500" : "fill-dark-gray"}'/>
        </NavLink>
    </NavGroup>
</div>