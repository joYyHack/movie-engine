import SearchIcon from "@mui/icons-material/Search";
import { Autocomplete, TextField } from "@mui/material";
import Box from "@mui/material/Box";
import IconButton from "@mui/material/IconButton";
import { Dispatch, SetStateAction, useState } from "react";
import { LastSearchResult } from "../utils/types";

const SearchBox = (props: {
  searchTitle: string;
  lastSearchResults: LastSearchResult[];
  setSearchValue: Dispatch<SetStateAction<string>>;
}) => {
  const [localSearchValue, setLocalSearchValue] = useState<string>(
    props.searchTitle
  );

  const handleSearchClick = async () => {
    props.setSearchValue(localSearchValue?.trim());
  };

  return (
    <Box
      sx={{ flexGrow: 1 }}
      className="bg-light rounded d-flex"
      id="testestesrt"
    >
      <Autocomplete
        freeSolo
        id="search-field"
        sx={{ ml: 1, flex: 1 }}
        options={
          props.lastSearchResults
            ?.sort(
              (a, b) => Date.parse(b.lastSearched) - Date.parse(a.lastSearched)
            )
            .map((option) => option.movieTitle) || []
        }
        onInputChange={(_, newInputValue) => {
          setLocalSearchValue(newInputValue);
        }}
        renderInput={(params) => (
          <TextField
            {...params}
            id="search-field"
            // sx={{ ml: 1, flex: 1 }}
            variant="standard"
            label="Movie title"
            placeholder="Search..."
            autoFocus={true}
            onChange={(e) => setLocalSearchValue(e.target.value)}
          />
        )}
      />
      <IconButton
        type="button"
        sx={{ p: "10px" }}
        aria-label="search"
        onClick={handleSearchClick}
      >
        <SearchIcon />
      </IconButton>
    </Box>
  );
};

export default SearchBox;
