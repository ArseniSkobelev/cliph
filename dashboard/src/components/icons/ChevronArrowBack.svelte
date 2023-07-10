<script>
    export let clickProp = () => {}

    export let stroke = true;

    export let strokeColor = "#ffffff";
    export let color;

    export let className;
</script>

<svg width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg" on:click={clickProp}
     class={className}>
<g clip-path="url(#clip0_1_3)">
<circle cx="16" cy="16" r="15.5" fill='{color ? color : "#4e3e80"}' stroke={stroke ? strokeColor : ""}/>
<path d="M18.9729 9.25406C19.1576 9.43882 19.25 9.65757 19.25 9.91032C19.25 10.1631 19.1576 10.3816 18.9729 10.5658L13.5595 15.9792L18.9913 21.4111C19.1638 21.5835 19.25 21.7991 19.25 22.0577C19.25 22.3164 19.1576 22.5381 18.9729 22.7229C18.7881 22.9076 18.5694 23 18.3166 23C18.0639 23 17.8454 22.9076 17.6611 22.7229L11.4532 16.4965C11.3793 16.4226 11.3269 16.3426 11.2958 16.2564C11.2648 16.1701 11.2495 16.0778 11.25 15.9792C11.25 15.8807 11.2655 15.7883 11.2966 15.7021C11.3276 15.6159 11.3798 15.5358 11.4532 15.4619L17.6796 9.23559C17.852 9.06315 18.0646 8.97693 18.3173 8.97693C18.5701 8.97693 18.7886 9.0693 18.9729 9.25406Z" fill="white"/>
</g>
<defs>
<clipPath id="clip0_1_3">
<rect width="32" height="32" fill="white"/>
</clipPath>
</defs>
</svg>
