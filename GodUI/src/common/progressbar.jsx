import { Box, LinearProgress, Typography } from '@mui/material';
import * as React from 'react';



const ProgressBar =({label,progress})=> {
    return (
    <Box sx={{ width: '100%' }}>
      <LinearProgress variant="determinate" value={progress||0} />
      <Typography sx={{ m:1 }} variant="body2" color="text.secondary">{label}</Typography>
    </Box>
  );
}

export default ProgressBar;
