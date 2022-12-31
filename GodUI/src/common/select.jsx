import * as React from 'react';
import Box from '@mui/material/Box';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';

export default function BasicSelect({value,label,onChange,menuItemList}) {
  
  return (
    <Box  sx={{ minWidth: 120,m: 2 }}>
      <FormControl fullWidth>
        <InputLabel id="demo-simple-select-label">{label}</InputLabel>
        <Select
          labelId="demo-simple-select-label"
          id="demo-simple-select"
          value={value}
          label={label}
          onChange={onChange}
        >
         {menuItemList.map(menu=><MenuItem key={menu.id} value={menu.id}>{menu.Name}</MenuItem>)}
        </Select>
      </FormControl>
    </Box>
  );
}
