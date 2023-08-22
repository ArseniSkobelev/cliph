<script lang="ts">
    export let groupName: string = "New group";
</script>

<div class="flex flex-col">
    <span class="mb-4 font-bold text-sm">{groupName}</span>
    <div class="flex flex-col gap-y-6">
        <slot/>
    </div>
</div>