<script lang="ts">
    export let width: number = 128;
    export let height: number = 128;
    export let className: string = "";
    export let dotClass: string = "#9E9E9E";
    export let lineClass: string = "#9E9E9E";
    export let circleClass: string = "#9E9E9E";
</script>

<svg {width} {height} class={className} viewBox="0 0 128 128" fill="none" xmlns="http://www.w3.org/2000/svg">
    <path fill-rule="evenodd" clip-rule="evenodd" class={lineClass}
          d="M64 91C65.3261 91 66.5979 90.4732 67.5355 89.5355C68.4732 88.5978 69 87.3261 69 86C69 84.6739 68.4732 83.4021 67.5355 82.4644C66.5979 81.5268 65.3261 81 64 81C62.6739 81 61.4021 81.5268 60.4645 82.4644C59.5268 83.4021 59 84.6739 59 86C59 87.3261 59.5268 88.5978 60.4645 89.5355C61.4021 90.4732 62.6739 91 64 91Z"
    />
    <path d="M64 41V73" stroke-width="8" class={dotClass} stroke-linecap="round"
          stroke-linejoin="round"/>
    <circle cx="64" cy="64" r="43.5" class={circleClass} stroke="white" stroke-width="9"/>
</svg>
