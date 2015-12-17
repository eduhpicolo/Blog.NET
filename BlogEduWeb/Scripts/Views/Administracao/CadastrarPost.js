$(document).ready(function () {
    var Tags = new Array();
    $("#adicionar").on('click', function () {
        
        var textoTag = $('#Tag').val();

        if (textoTag.trim() === '') {
            alert("O campo TAG é Obrigatório.");
            $('#Tag').focus();
            return;
        }
        var existe = Tags.filter(function (v) {
            return v.Tag.toLowerCase() === textoTag.toLowerCase();
        })[0];

        if (existe != undefined) {
            alert('Esta TAG já foi informada.');
            return;
        }

        Tags.push(new Object({ Tag: textoTag }));
        montaListaPeloArray();
        //$('#resultado').append('<li>' + textoTag + '</li>');
        //var form = $('form');
        //form.append('<input name="Tags" type="hidden" value ="' + textoTag + '"/>');

        $('#Tag').val('');

        $('#Tag').focus();
    });

    function montaListaPeloArray() {
        var form = $('form');
        
        $('input[name="Tags"]').remove();
        $('#resultado').empty();

        $(Tags).each(function (){
            $('#resultado').append('<li><span>' + this.Tag + '</span><a tag="' + this.Tag + '"class="remover-tag icone-excluir"></a></li>');
            form.append('<input name="Tags" type="hidden" value ="' + this.Tag + '"/>');
        });
    }
    
    $('body').on('click', '.remover-tag', function () {
        var tag = $(this).attr('tag');

        Tags = $.grep(Tags, function (e) {
            return e.Tag !== tag;
        })

        montaListaPeloArray();
    });

    $("input[Name='Tags']")
    .map(function () {
        var tag = $(this).val();
        Tags.push(new Object({ Tag: tag }))
    }).get();
});
