import { useEffect, useId, useRef, useState } from "react";
import { axiosJWT, url } from "../../utils/httpRequest";
import { useDispatch, useSelector } from "react-redux";
import { selectAccessToken, selectRefreshToken, selectUser } from "../../redux/selectors";
import { toast } from "react-toastify";

const defaultFn = () => {};

export default function PostComment({ posted = true, comment, bookedRoomId, onPostCommentClick = defaultFn }) {
	const currentUser = useSelector(selectUser);
	const accessToken = useSelector(selectAccessToken);
	const refreshToken = useSelector(selectRefreshToken);
	const dispatch = useDispatch();
	const starRef = useRef(0);
	const contentRef = useRef();

	const starsRef = useRef();

	const [errorMessage, setErrorMessage] = useState("");

	const getMap = () => {
		if (!starsRef.current) {
			// Initialize the Map on first usage.
			starsRef.current = new Map();
		}
		return starsRef.current;
	};

	const getNode = (id, node) => {
		const map = getMap();
		if (node) {
			map.set(id, node);
		} else {
			map.delete(id);
		}
	};

	const handleRateStar = (star) => {
		starRef.current = star;
		const map = getMap();
		if (star === 0) {
			map.forEach((node) => {
				node.classList.remove("star-active");
			});
		} else {
			map.forEach((node, key) => {
				if (key <= star) {
					node.classList.add("star-active");
				} else {
					node.classList.remove("star-active");
				}
			});
		}
	};
	const handleClick = () => {
		if (starRef.current === 0) {
			setErrorMessage("Vui lòng chọn số sao!");
		} else {
			setErrorMessage("");
			onPostCommentClick();
			if (posted) {
				handleUpdate();
			} else {
				handlePost();
			}
		}
	};
	const handlePost = async () => {
		const axiosJwt = axiosJWT(accessToken, refreshToken, dispatch);
		const idToast = toast.loading("Đang xử lý!");
		try {
			const today = new Date();
			const res = await axiosJwt.post(
				url.postComment,
				{
					rating: starRef.current,
					content: contentRef.current.value,
					createDate: today.toISOString(),
					lastChange: today.toISOString(),
					bookingId: bookedRoomId,
				},
				{
					headers: {
						Authorization: "Bearer " + accessToken,
					},
				},
			);
			if (res.data.success) {
				toast.update(idToast, {
					render: "Đăng bình luận thành công!",
					type: "success",
					closeButton: true,
					autoClose: 1000,
					isLoading: false,
				});
			}
		} catch (error) {
			console.log(error);
			toast.update(idToast, {
				render: "Đăng bình luận thất bại!",
				type: "error",
				closeButton: true,
				autoClose: 1000,
				isLoading: false,
			});
		}
	};
	const handleUpdate = async () => {
		const axiosJwt = axiosJWT(accessToken, refreshToken, dispatch);
		const idToast = toast.loading("Đang xử lý!");
		try {
			const res = await axiosJwt.put(
				url.updateComment,
				{
					id: comment.id,
					rating: starRef.current,
					content: contentRef.current.value,
					createDate: comment.createDate,
					lastChange: new Date().toISOString(),
					bookingId: bookedRoomId,
				},
				{
					headers: {
						Authorization: "Bearer " + accessToken,
					},
				},
			);
			if (res.data.success) {
				toast.update(idToast, {
					render: "Cập nhật bình luận thành công!",
					type: "success",
					closeButton: true,
					autoClose: 1000,
					isLoading: false,
				});
			}
		} catch (error) {
			console.log(error);
			toast.update(idToast, {
				render: "Cập nhật bình luận thất bại!",
				type: "error",
				closeButton: true,
				autoClose: 1000,
				isLoading: false,
			});
		}
	};
	useEffect(() => {
		handleRateStar(comment?.rating || 0);
	}, []);
	return (
		<div className="d-flex flex-column">
			<div className="d-flex gap-4 p-3 border-top border-bottom border-2">
				<div
					className="d-flex flex-column"
					style={{
						// flexBasis: "25%",
						minWidth: "25%",
					}}>
					<div className="d-flex flex-column">
						<div className="d-flex align-items-center">
							<div
								className="me-2 p-1 border border-3 rounded-circle"
								style={{
									width: 64,
									height: 64,
								}}>
								<img className="w-100 h-100" src="/img/user-avatar.png" alt="avatar" />
							</div>
							<div className="d-flex flex-column flex-grow-1 fw-bold">
								{currentUser.lastName + " " + currentUser.firstName}
							</div>
						</div>
					</div>
				</div>
				<div className="d-flex flex-column gap-2 flex-grow-1">
					<div className="d-flex justify-content-between" style={{ cursor: "pointer" }}>
						<div className="d-flex align-items-center justify-content-between">
							{Array.from({ length: 5 }).map((_, index) => (
								<i
									key={index}
									ref={(node) => {
										getNode(index + 1, node);
									}}
									className="fa-solid fa-star rating-star star-no"
									data-index={index + 1}
									onClick={() => {
										handleRateStar(index + 1);
									}}></i>
							))}
						</div>
						{/* <div className="text-secondary">16 May 23</div> */}
					</div>
					<div className="d-flex flex-column mt-2">
						<div>
							<input
								ref={contentRef}
								className="form-control"
								defaultValue={comment?.content}
								placeholder="Để lại bình luận của bạn..."
							/>
						</div>
					</div>
					<div className="d-flex align-items-center justify-content-between">
						<button type="button" className="btn btn-primary" onClick={handleClick}>
							{posted ? "Cập nhật" : "Đăng"}
						</button>
						{errorMessage && <label className="alert alert-danger m-0 py-1">{errorMessage}</label>}
					</div>
				</div>
			</div>
		</div>
	);
}
