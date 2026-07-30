# Copilot Instructions

## Directrices del proyecto
- Mapeo de campos/formularios: `frm_socio` usa numero,nombre,apellidos,dni,direccion,cp,localidad,provincia,pais,fechanac,email,tipo_socio,comentarios. Edad >=65 => rdo_jubilado; edad <16 => rdo_otros. `frm_federativas` usa numero,nombre,apellidos,dni,fechanac,direccion,cp,localidad; si edad <16 => rdo_gratis; telefono y comentarios se rellenan después; `cmb_compite` y `cmb_modalidad` se rellenan manualmente.