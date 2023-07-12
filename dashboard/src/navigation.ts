interface NavLink {
    title: string,
    path: string,
    imagePath: string,
    gap: boolean | undefined
}

export let NavigationLinks : NavLink[] = [
    {title: "Dashboard", path: "/", imagePath: "/icons/ic_round-dashboard.png", gap: undefined},
    {title: "Users", path: "/users", imagePath: "/icons/mdi_users.png", gap: undefined},
    {title: "API keys", path: "/apikeys", imagePath: "/icons/ph_wrench-fill.png", gap: undefined},
    {title: "Settings", path: "/settings", imagePath: "/icons/heroicons-solid_cog.png", gap: undefined}
]