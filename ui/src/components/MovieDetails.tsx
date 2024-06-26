import CloseIcon from "@mui/icons-material/Close";
import StarBorderIcon from "@mui/icons-material/StarBorder";
import { CircularProgress, Rating } from "@mui/material";
import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import IconButton from "@mui/material/IconButton";
import { styled } from "@mui/material/styles";
import Typography from "@mui/material/Typography";
import { Dispatch, SetStateAction } from "react";
import { MovieFullData } from "../utils/types";

const BootstrapDialog = styled(Dialog)(({ theme }) => ({
  "& .MuiDialog-paper": {
    backgroundColor: "#333",
    color: "#fff",
  },

  "& .MuiDialogContent-root": {
    padding: theme.spacing(2),
  },
  "& .MuiDialogActions-root": {
    padding: theme.spacing(1),
  },
  "& .MuiDialogTitle-root": {
    padding: theme.spacing(2),
    paddingRight: theme.spacing(5),
  },
  "& .MuiTypography-root": {
    whiteSpace: "pre-line",
  },
}));

const CloseButton = styled(IconButton)(({ theme }) => ({
  position: "absolute",
  right: theme.spacing(1),
  top: theme.spacing(1),
  color: theme.palette.grey[500],
}));

const MovieDetails = (props: {
  movieDetailsLoading: boolean;
  movieDetails: MovieFullData | undefined;
  openModal: boolean;
  setOpenModal: Dispatch<SetStateAction<boolean>>;
}) => {
  const handleClose = () => {
    props.setOpenModal(false);
  };

  return (
    <BootstrapDialog
      onClose={handleClose}
      aria-labelledby="customized-dialog-title"
      open={props.openModal}
      maxWidth="lg"
      fullWidth
    >
      {props.movieDetailsLoading ? (
        <div
          style={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            height: "100px",
            width: "100px",
          }}
        >
          <CircularProgress color="inherit" />
        </div>
      ) : (
        <>
          <DialogTitle sx={{ m: 0, p: 2 }} id="customized-dialog-title">
            {props.movieDetails?.title || "Movie Details"}
            <CloseButton aria-label="close" onClick={handleClose}>
              <CloseIcon />
            </CloseButton>
            <div>
              <Rating
                name="movie-rating"
                value={Number(props.movieDetails?.imdbRating) || 0}
                max={10}
                precision={0.5}
                size="large"
                readOnly
                emptyIcon={
                  <StarBorderIcon
                    style={{ color: "#ccc" }}
                    fontSize="inherit"
                  />
                }
                sx={{ verticalAlign: "middle" }}
              />
            </div>
          </DialogTitle>
          <DialogContent dividers>
            <Typography variant="body1" gutterBottom>
              <strong>Title:</strong> {props.movieDetails?.title}
            </Typography>
            <Typography variant="body1" gutterBottom>
              <strong>Year:</strong> {props.movieDetails?.year}
            </Typography>
            <Typography variant="body1" gutterBottom>
              <strong>Genre:</strong> {props.movieDetails?.genre}
            </Typography>
            <Typography variant="body1" gutterBottom>
              <strong>Director:</strong> {props.movieDetails?.director}
            </Typography>
            <Typography variant="body1" gutterBottom>
              <strong>Plot:</strong> {props.movieDetails?.plot}
            </Typography>
            <Typography variant="body1" gutterBottom>
              <strong>Actors:</strong> {props.movieDetails?.actors}
            </Typography>
            <Typography variant="body1" gutterBottom>
              <strong>Country:</strong> {props.movieDetails?.country}
            </Typography>
            <Typography variant="body1" gutterBottom>
              <strong>Language:</strong> {props.movieDetails?.language}
            </Typography>
          </DialogContent>
        </>
      )}
    </BootstrapDialog>
  );
};

export default MovieDetails;
