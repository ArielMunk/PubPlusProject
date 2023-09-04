import React, { useEffect, useState } from "react";
import {
  OutlinedInput,
  InputLabel,
  MenuItem,
  Select,
  FormControl
} from "@mui/material";

export default function MultiSelect(props) {
  const [title,SetTitle] = useState(props.options.title);
  const [selectedNames, setSelectedNames] = useState([]);
  const multiSelect = typeof(props.options.multiSelect)!="undefined"?props.options.multiSelect:false;
  
  const names = props.options.names;
  if(typeof(names)=="undefined"){
    names =[] ;
  }
  return (
    <FormControl sx={{ m: 1, width: 500 }}>
      <InputLabel>{title}</InputLabel>
      <Select
        multiple={multiSelect}
        value={selectedNames}
        onChange={(e) => {
          setSelectedNames(e.target.value);
          if(typeof(props.options.ChangeHandler)!=="undefined"){
            props.options.ChangeHandler(e);
          }
          }}
        input={<OutlinedInput label={title} />}
        >
        {  
        Object.entries(names).map(([id, name]) => (
          <MenuItem key={name} value={id}>
            {name}
          </MenuItem>
        ))}
      </Select>
    </FormControl>
  );

}