import * as React from 'react';
import * as styles from './Modal.css';

export type ModalProps = {
    title?: string;
    confirmLabel: string;
    onClose(): void;
    onConfirm(): void;
};

export const Modal: React.FC<ModalProps> = props => {
    return (
        <div className={styles.modal}>
            <section className={styles.modalMain}>
                <div>title: {props.title}</div>
                {props.children}
                <div>
                    <button onClick={_ => props.onClose()}>Close</button>
                </div>
                <div>
                    <button onClick={_ => props.onConfirm()}>{props.confirmLabel}</button>
                </div>
            </section>
        </div>
    );
};
