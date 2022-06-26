<template>

    <div class="vue-magnifier-container">
        <slot></slot>
        <span ref="magnificationElement" class="preview tw-w-full tw-h-full" v-bind:style="{backgroundImage:'url('+src+')'}">
            <span ref="glass" class="magnifying-glass" v-bind:style="{backgroundImage:'url('+srcLarge+')', backgroundPosition: backgroundPos, left: cursorX +'px', top: cursorY+'px'}"></span>
        </span>
    </div>

</template>

<script>
export default {
props: {
        src: String,
        srcLarge: String
    },
    data(){
        return {
            img: null,
            width: null,
            height: null,
            bounds: null,
            cursorX:0,
            cursorY:0,
            thumbPos:{x:0,y:0},
            backgroundPos: '0 0'
        }
    },
    methods: {
        getCursorPos: function(e){
            const me = this
            const x = (window.Event) ? e.pageX : event.clientX + (document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft);
            const y = (window.Event) ? e.pageY : event.clientY + (document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop);
            me.cursorX = x-me.thumbPos.x;
            me.cursorY = y-me.thumbPos.y;
        },
        getBounds: function(){
            const me=this
            let el = me.$refs.magnificationElement;

            me.bounds = el.getBoundingClientRect();

            let xPos = 0;
            let yPos = 0;
            while (el) {
                const transform = me.getTransform(el);
                if (el.tagName == "BODY") {
                    // deal with browser quirks with body/window/document and page scroll
                    const xScroll = el.scrollLeft || document.documentElement.scrollLeft;
                    const yScroll = el.scrollTop || document.documentElement.scrollTop;


                    xPos += (el.offsetLeft - xScroll + el.clientLeft + parseInt(transform[0]));
                    yPos += (el.offsetTop - yScroll + el.clientTop + parseInt(transform[1]));
                } else {
                    // for all other non-BODY elements
                    xPos += (el.offsetLeft - el.scrollLeft + el.clientLeft + parseInt(transform[0]));
                    yPos += (el.offsetTop - el.scrollTop + el.clientTop + parseInt(transform[1]));
                }

                el = el.offsetParent;
            }
            me.thumbPos = {
                x: xPos,
                y: yPos
            }
        },
        moveMagnifier: function (e) {
            const me=this
            e.preventDefault();

            me.getBounds();
            me.getCursorPos(e);
            me.backgroundPos = "10% 10%";
        },
        getTransform: function (el) {
            const transform = window.getComputedStyle(el, null).getPropertyValue('-webkit-transform');

            function rotate_degree(matrix) {
                let angle=0
                if(matrix !== 'none') {
                    const values = matrix.split('(')[1].split(')')[0].split(',');
                    const a = values[0];
                    const b = values[1];
                    angle = Math.round(Math.atan2(b, a) * (180/Math.PI));
                } else {
                    angle = 0;
                }
                return (angle < 0) ? angle +=360 : angle;
            }

            const results = transform.match(/matrix(?:(3d)\(-{0,1}\d+\.?\d*(?:, -{0,1}\d+\.?\d*)*(?:, (-{0,1}\d+\.?\d*))(?:, (-{0,1}\d+\.?\d*))(?:, (-{0,1}\d+\.?\d*)), -{0,1}\d+\.?\d*\)|\(-{0,1}\d+\.?\d*(?:, -{0,1}\d+\.?\d*)*(?:, (-{0,1}\d+\.?\d*))(?:, (-{0,1}\d+\.?\d*))\))/);

            var output = [0,0,0];
            if(results){
                if(results[1] == '3d'){
                    output = results.slice(2,5);
                } else {
                    results.push(0);
                    output = results.slice(5, 9); // returns the [X,Y,Z,1] value;
                }

                output.push(rotate_degree(transform));
            }
            return output;
        }
    },
    mounted: function(){
        const me=this
        me.$nextTick(function () {
            const el=me.$refs.magnificationElement
            console.log(el);
            el.addEventListener("mousemove", me.moveMagnifier);
        })
    },
    
}
</script>

<style lang="scss" scoped>
// Magnifying glass options
$border-size: 5px; // Modify the border width of the magnifying glass component
$border-color: #666666; // Modify the border color of the magnifying glass component
$magnifier-width: 150px; // Modify the width of the magnifying glass component
$magnifier-height: 150px; // Modify the height of the magnifying glass component

// Define your responsive sizes of 
$sizes: (
    '(max-width: 320px)' 250px 250px,
    '(max-width: 480px)' 350px 350px,
    '(min-width: 481px)' 450px 450px,
    '(min-width: 1024px)' 550px 550px,
    '(min-width: 1280px)' 600px 600px,
);

.vue-magnifier-container {
    position: relative;
    .preview {
        position:relative;
        background-repeat: no-repeat;
        background-position:  50% 50%;
        background-size: cover;
        display:block;
        clear: both;
        margin:0 auto;
        cursor:none;

        .magnifying-glass {
            position: absolute;
            border: $border-size solid $border-color;
            border-radius: 50%;
            cursor: none;
            width: $magnifier-width;
            height: $magnifier-height;
            transform:translate((-1*$magnifier-width/2),(-1*$magnifier-width/2));
            background:#000 no-repeat;
            display:none;
            pointer-events: none;
        }

        &:hover {
            .magnifying-glass {
                display:block;
            }
        }

        // @each $breakpoint in $sizes {
        //     $query: nth($breakpoint,1);
        //     $bpWidth: nth($breakpoint,2);
        //     $bpHeight: nth($breakpoint,3);

        //     @media only screen and #{$query} {
        //         width: $bpWidth;
        //         height: $bpHeight;
        //     }
        // }
    }
}
</style>