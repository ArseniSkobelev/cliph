<script>
    import "../../app.css";

    import CliphLogo from "/src/components/icons/CliphLogo.png";
</script>

<div class="flex flex-col items-center justify-center h-full">
    <div class="flex flex-col gap-y-4 items-center justify-center h-2/5">
        <img src="{CliphLogo}" alt="Logo" width="128" height="128">
        <h1 class="text-2xl">Authentication</h1>
    </div>
    <div class="h-3/5 w-80">
        <slot />
    </div>
</div>