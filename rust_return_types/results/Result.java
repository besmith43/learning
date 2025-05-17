
public class Result<T, E extends Exception> {
    private T value;
    private E error;
    private boolean isSuccess;

    private Result(T value, E error, boolean isSuccess) {
        this.value = value;
        this.error = error;
        this.isSuccess = isSuccess;
    }

    public static <T, E extends Exception> Result<T, E> success(T value) {
        return new Result<>(value, null, true);
    }

    public static <T, E extends Exception> Result<T, E> failure(E error) {
        return new Result<>(null, error, false);
    }

    public boolean isSuccess() {
        return isSuccess;
    }

    public T getValue() {
        return value;
    }

    public E getError() {
        return error;
    }
}
