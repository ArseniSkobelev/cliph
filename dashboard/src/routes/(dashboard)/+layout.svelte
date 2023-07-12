<script>
    import "../../app.css";
    import DashboardLayout from "../../components/DashboardLayout.svelte";
</script>

<DashboardLayout>
    <div class="w-full h-full p-6">
        <slot />
    </div>
</DashboardLayout>