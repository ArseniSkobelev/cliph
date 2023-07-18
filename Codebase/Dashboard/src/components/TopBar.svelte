<script lang="ts">
    import {page} from "$app/stores";

    const capitalize = (pageName: string): string => {
        return pageName.charAt(0).toUpperCase() + pageName.substring(1, pageName.length);
    }
</script>

<div class="py-8 px-8">
    <span
            class="text-4xl font-semibold">{$page.url.pathname.length == 1 ? "Dashboard" :
        capitalize($page.url.pathname.substring(1, $page.url.pathname.length))}</span>
</div>