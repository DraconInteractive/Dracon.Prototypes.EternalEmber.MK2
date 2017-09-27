// https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting.html
var MinibufferWebPlugin = {
    div: null,
    SetHTML: function(str)
    {
        // How do I add an element without jquery?
        // http://stackoverflow.com/questions/16063806/add-to-dom-without-jquery
        var div = document.getElementById("minibuffer-output");
        if (div == null) {
            div = document.createElement("div");
            div.id = "minibuffer-output"
            div.style.marginTop = "800px";
            div.style.marginLeft = "1in";
            div.style.marginRight = "1in";
            div.style.marginBottom = "1in";
            document.getElementsByTagName('body')[0].appendChild(div);

            // We want to change Canvas' position from 50% to 4em.
            // .template-wrap { position: absolute; top:4em; left: 50%; -webkit-transform: translate(-50%, 0); transform: translate(-50%, 0); }
            var canvasDiv = document.getElementsByTagName('canvas')[0].parentElement;
            canvasDiv.style.top = "4em";
            canvasDiv.style["-webkit-background-size"] = "translate(-50%, 0)";
            canvasDiv.style.transform = "translate(-50%, 0)";

        }
        div.innerHTML = Pointer_stringify(str);
    },
    Hello: function()
    {
        window.alert("Hello, world!");
    },
    HelloString: function(str)
    {
        window.alert(Pointer_stringify(str));
    },
    PrintFloatArray: function(array, size)
    {
        for(var i=0;i<size;i++)
            console.log(HEAPF32[(array>>2)+size]);
    },
    AddNumbers: function(x,y)
    {
        return x + y;
    },
    StringReturnValueFunction: function()
    {
        var returnStr = "bla";
        var buffer = _malloc(lengthBytesUTF8(returnStr) + 1);
        writeStringToMemory(returnStr, buffer);
        return buffer;
    },
    BindWebGLTexture: function(texture)
    {
        GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[texture]);
    }
};

mergeInto(LibraryManager.library, MinibufferWebPlugin);
