import { Box, TextField } from "@mui/material";

const TextFieldInput = ({id,label,value,required=false,name,onValueChange}) => {


    return ( 
      
      <Box sx={{ m: 2,
        width: '100%' 
      }} 
      >
        <TextField
        sx={{width:'100%'}}
        required={required}
        id={id}
        name ={name}
        onChange={onValueChange}
        label={label}
        value={value}
      />
      </Box>
     );
}
 
export default TextFieldInput;